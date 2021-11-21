using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object
{
    public class MessageLogin
    {
        private int status;
        private string notification;
        private string userId;

        public MessageLogin(int status, string notification, string userId)
        {
            this.status = status;
            this.notification = notification;
            this.userId = userId;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string UserId { get => userId; set => userId = value; }
    }
}
