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

    public class GetCountRating
    {
        public string count { get; set; }
        public string count1 { get; set; }
        public string count2 { get; set; }
        public string count3 { get; set; }
        public string count4 { get; set; }
        public string count5 { get; set; }
    }

    public class Message_Rate
    {
        private int status;
        private string notification;
        private string rateTotal;
        private GetCountRating countRating;
        private IEnumerable<GetRating> rates;

        public Message_Rate(int status, string notification, string rateTotal, GetCountRating countRating, IEnumerable<GetRating> rates)
        {
            this.status = status;
            this.notification = notification;
            this.rateTotal = rateTotal;
            this.countRating = countRating;
            this.rates = rates;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string RateTotal { get => rateTotal; set => rateTotal = value; }
        public GetCountRating CountRating { get => countRating; set => countRating = value; }
        public IEnumerable<GetRating> Rates { get => rates; set => rates = value; }
    }

    public class GetReview
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int value { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public string imageRes { get; set; }
        public string date { get; set; }
        public string UserName { get; set; }
        public string imageUser { get; set; }
        public long ? countLike { get; set; }
        public List<string> imgList { get; set; }
        public List<Get.GetLike> userList { get; set; }
        public List<Get.GetComment> comments { get; set; }
    }

    public class Message_Review
    {
        private int status;
        private string notification;
        private string reviewTotal;
        private GetCountRating countRating;
        private IEnumerable<GetReview> reviews;

        public Message_Review(int status, string notification, string reviewTotal, GetCountRating countRating, IEnumerable<GetReview> reviews)
        {
            this.status = status;
            this.notification = notification;
            this.reviewTotal = reviewTotal;
            this.countRating = countRating;
            this.reviews = reviews;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string ReviewTotal { get => reviewTotal; set => reviewTotal = value; }
        public GetCountRating CountRating { get => countRating; set => countRating = value; }
        public IEnumerable<GetReview> Reviews { get => reviews; set => reviews = value; }
    }
}
