using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetFood
    {
        public string foodId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string unit { get; set; }
        public string menuName { get; set; }
        public string categoryName { get; set; }
        public List<string> pic { get; set; }
    }

    public class Message_Food
    {
        private int status;
        private string notification;
        private GetFood food;

        public Message_Food(int status, string notification, GetFood food)
        {
            this.status = status;
            this.notification = notification;
            this.food = food;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetFood Food { get => food; set => food = value; }
    }


    public class Message_FoodList
    {
        private int status;
        private string notification;
        private GetReserveTable reserveTable;
        private IEnumerable<GetFood> foodList;

        public Message_FoodList(int status, string notification, GetReserveTable reserveTable, IEnumerable<GetFood> foodList)
        {
            this.status = status;
            this.notification = notification;
            this.reserveTable = reserveTable;
            this.foodList = foodList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetReserveTable ReserveTable { get => reserveTable; set => reserveTable = value; }
        public IEnumerable<GetFood> FoodList { get => foodList; set => foodList = value; }
    }
}
