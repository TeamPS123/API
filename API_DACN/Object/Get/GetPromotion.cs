using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetPromotion
    {
        public string promotionId { get; set; }
        public string restaurantName { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string value { get; set; }
        public bool ? status { get; set; }
        public string line { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string image { get; set; }
        public double ? distance { get; set; }
    }

    public class Message_ProList
    {
        private int status;
        private string notification;
        private IEnumerable<GetPromotion> proList;

        public Message_ProList(int status, string notification, IEnumerable<GetPromotion> proList)
        {
            this.status = status;
            this.notification = notification;
            this.proList = proList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetPromotion> ProList { get => proList; set => proList = value; }
    }

    public class GetPromotion1
    {
        public string promotionId { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string value { get; set; }
        public bool ? status { get; set; }
    }

    public class Message_Promotion
    {
        private int status;
        private string notification;
        private IEnumerable<GetPromotion1> proList;

        public Message_Promotion(int status, string notification, IEnumerable<GetPromotion1> proList)
        {
            this.status = status;
            this.notification = notification;
            this.proList = proList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetPromotion1> ProList { get => proList; set => proList = value; }
    }
}
