using Journal.Notification;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Journal.API
{
    public class NotificationFilter : IActionFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notificationContext.HasNotifications)
            {
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
