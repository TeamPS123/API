using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetStatis
    {
        public string amountWait { get; set; }
        public string amountDeny { get; set; }
        public string amountConfirm { get; set; }
        public string amountComplete { get; set; }
        public string amountExpired { get; set; }
    }

    public class Message_Statis
    {
        private int status;
        private string notification;
        private GetStatis getStatic;

        public Message_Statis(int status, string notification, GetStatis getStatic)
        {
            this.status = status;
            this.notification = notification;
            this.getStatic = getStatic;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetStatis GetStatic { get => getStatic; set => getStatic = value; }
    }
}
