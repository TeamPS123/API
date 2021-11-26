using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetMenu
    {
        public string menuId { get; set; }
        public string name { get; set; }
        public IEnumerable<FoodOfMenu> foodList { get; set; }
    }

    public class FoodOfMenu
    {
        public string menuId { get; set; }
        public string foodId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string unit { get; set; }
        public string categoryName { get; set; }
        public List<string> pic {get; set;}
    }

    public class Message_Menu
    {
        private int status;
        private string notification;
        private GetMenu menu;

        public Message_Menu(int status, string notification, GetMenu menu)
        {
            this.status = status;
            this.notification = notification;
            this.menu = menu;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public GetMenu Menu { get => menu; set => menu = value; }
    }

    public class Message_MenuList
    {
        private int status;
        private string notification;
        private IEnumerable<GetMenu> menuList;

        public Message_MenuList(int status, string notification, IEnumerable<GetMenu> menuList)
        {
            this.status = status;
            this.notification = notification;
            this.menuList = menuList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetMenu> MenuList { get => menuList; set => menuList = value; }
    }
}
