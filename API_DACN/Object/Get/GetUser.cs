using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetUser
    {
        public string fullName { get; set; }
        public string phone { get; set; }
        public bool isBusiness { get; set; }
        public bool? gender { get; set; }
        public string pic { get; set; }
    }

    public class MessageUser
    {
        private int status;
        private string notification;
        private GetUser user;

        public MessageUser(int status, string notification, GetUser user)
        {
            this.status = status;
            this.notification = notification;
            this.user = user;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetUser User { get => user; set => user = value; }
    }
}
