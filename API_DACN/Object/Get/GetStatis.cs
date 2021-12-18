using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetStatis
    {
        public string time { get; set; }
        public string amountComplete { get; set; }
        public string amountExpired { get; set; }
    }

    public class Message_Statis
    {
        private int status;
        private string notification;
        private List<GetStatis> getStatis;

        public Message_Statis(int status, string notification, List<GetStatis> getStatis)
        {
            this.status = status;
            this.notification = notification;
            this.getStatis = getStatis;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public List<GetStatis> GetStatis { get => getStatis; set => getStatis = value; }
    }
}
