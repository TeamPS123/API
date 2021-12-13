using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetRating
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int value { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public string date { get; set; }
        public string UserName { get; set; }
        public string imageUser { get; set; }
    }

    public class Message_Rate
    {
        private int status;
        private string notification;
        private string rateTotal;
        private IEnumerable<GetRating> rates;

        public Message_Rate(int status, string notification, string rateTotal, IEnumerable<GetRating> rates)
        {
            this.status = status;
            this.notification = notification;
            this.rateTotal = rateTotal;
            this.rates = rates;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string RateTotal { get => rateTotal; set => rateTotal = value; }
        public IEnumerable<GetRating> Rates { get => rates; set => rates = value; }
    }
}
