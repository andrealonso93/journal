using System.Net;

namespace Journal.Notification
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
           _notifications = new List<Notification>();
        }

        public void AddNotification(HttpStatusCode httpStatusCode, string message)
        {
            _notifications.Add(new Notification { HttpStatusCode = httpStatusCode, Message = message });
        }

        public void AddNotification(int statusCode, string message)
        {
            if(!Enum.TryParse<HttpStatusCode>(statusCode.ToString(), out var httpStatusCode))
                httpStatusCode = HttpStatusCode.InternalServerError;

            _notifications.Add(new Notification { HttpStatusCode = httpStatusCode, Message = message });
        }
    }
}
