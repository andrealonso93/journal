using Journal.Notification;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Journal.API.Filters
{
    public class NotificationFilter(NotificationContext _notificationContext, ILogger<NotificationFilter> _logger) : IActionFilter
    {
        private readonly NotificationContext _notificationContext = _notificationContext;
        private readonly ILogger<NotificationFilter> _logger = _logger;
        
        public async void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notificationContext.HasNotifications)
            {
                _logger.LogError("Request failed with notifications. The first added notification will be the response");

                var firstNotification = _notificationContext.Notifications.First();
                context.HttpContext.Response.StatusCode = (int)firstNotification.HttpStatusCode;
                context.HttpContext.Response.ContentType = "application/json";

                var notificationJson = JsonSerializer.Serialize(firstNotification);
                await context.HttpContext.Response.WriteAsync(notificationJson);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
