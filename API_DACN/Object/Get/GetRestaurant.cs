using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetRestaurant
    {
        public string restaurantId { get; set; }
        public string name { get; set; }
        public string line { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string longLat { get; set; }
        public string openTime { get; set; }
        public string closeTime { get; set; }
        public string distance { get; set; }
        public List<string> pic { get; set; }
        public List<string> categoryRes { get; set; }
    }

    public class Message_Res
    {
        private int status;
        private string notification;
        private GetRestaurant restaurant;

        public Message_Res(int status, string notification, GetRestaurant restaurant)
        {
            this.status = status;
            this.notification = notification;
            this.restaurant = restaurant;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetRestaurant Restaurant { get => restaurant; set => restaurant = value; }
    }

    public class Message_ResList
    {
        private int status;
        private string notification;
        private IEnumerable<GetRestaurant> resList;

        public Message_ResList(int status, string notification, IEnumerable<GetRestaurant> resList)
        {
            this.status = status;
            this.notification = notification;
            this.resList = resList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetRestaurant> ResList { get => resList; set => resList = value; }
    }
}
