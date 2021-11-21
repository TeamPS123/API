using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object
{
    public class Message
    {
        private int status;
        private string notification;
        private string id;

        public Message(int status, string notification, string id)
        {
            this.status = status;
            this.notification = notification;
            this.id = id;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string Id { get => id; set => id = value; }
    }
}
