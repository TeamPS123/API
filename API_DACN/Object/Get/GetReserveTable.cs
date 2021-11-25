using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetReserveTable
    {
        public string Id { get; set; }
        public int quantity { get; set; }
        public string time { get; set; }
        public string promotionId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string note { get; set; }
        public Get.GetRestaurant restaurant { get; set; }
    }

    public class Message_ReserveTable
    {
        private int status;
        private string notification;
        private IEnumerable<GetReserveTable> reserveTables;

        public Message_ReserveTable(int status, string notification, IEnumerable<GetReserveTable> reserveTables)
        {
            this.Status = status;
            this.Notification = notification;
            this.ReserveTables = reserveTables;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetReserveTable> ReserveTables { get => reserveTables; set => reserveTables = value; }
    }
}
