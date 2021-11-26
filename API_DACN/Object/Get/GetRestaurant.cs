using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetRestaurant
    {
        public string userId { get; set; }
        public string restaurantId { get; set; }
        public string name { get; set; }
        public string line { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string longLat { get; set; }
        public string openTime { get; set; }
        public string closeTime { get; set; }
        public string distance { get; set; }
        public string phoneRes { get; set; }
        public string mainPic { get; set; }
        public List<string> pic { get; set; }
        public string categoryResStr { get; set; }
        public IEnumerable<GetPromotion_Res> promotionRes { get; set; }
        public IEnumerable<GetCategoryRes> categoryRes { get; set; }
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
        private IEnumerable<GetCategoryRes> categoryList;
        private IEnumerable<string> districtList;

        //public Message_ResList(int status, string notification, IEnumerable<GetRestaurant> resList)
        //{
        //    this.status = status;
        //    this.notification = notification;
        //    this.resList = resList;
        //}

        public Message_ResList(int status, string notification, IEnumerable<GetRestaurant> resList, IEnumerable<GetCategoryRes> categoryList, IEnumerable<string> districtList)
        {
            this.status = status;
            this.notification = notification;
            this.resList = resList;
            this.categoryList = categoryList;
            this.districtList = districtList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetRestaurant> ResList { get => resList; set => resList = value; }
        public IEnumerable<GetCategoryRes> CategoryList { get => categoryList; set => categoryList = value; }
        public IEnumerable<string> DistrictList { get => districtList; set => districtList = value; }
    }

    public class GetPromotion_Res
    {
        public string id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string value { get; set; }
    }

    public class getInfoRes
    {
        public string name { get; set; }
        public string pic { get; set; }
    }

    public class MessageInfoRes
    {
        private int status;
        private string notification;
        private getInfoRes res;

        public MessageInfoRes(int status, string notification, getInfoRes res)
        {
            this.status = status;
            this.notification = notification;
            this.res = res;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public getInfoRes Res { get => res; set => res = value; }
    }
}
