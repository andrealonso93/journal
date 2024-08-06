using System.Net;

namespace Journal.Notification
{
    public class Notification
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
