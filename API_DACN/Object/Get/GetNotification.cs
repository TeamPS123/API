using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetNotification
    {
        public string resName { get; set; }
        public string img { get; set; }
        public string time { get; set; }
        public string userId { get; set; }
        public string reserveTableId { get; set; }
        public int status { get; set; }
        public string resId { get; set; }
    }

    public class Message_Notification
    {
        private int status;
        private string notification;
        private IEnumerable<GetNotification> notifications;

        public Message_Notification(int status, string notification, IEnumerable<GetNotification> notifications)
        {
            this.status = status;
            this.notification = notification;
            this.notifications = notifications;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetNotification> Notifications { get => notifications; set => notifications = value; }
    }
}
