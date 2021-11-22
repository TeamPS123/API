using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetCategoryRes
    {
        public int id { get; set; }
        public string  name { get; set; }
        public string icon { get; set; }
    }

    public class Message_CategoryResList
    {
        private int status;
        private string notification;
        private IEnumerable<GetCategoryRes> categoryResList;

        public Message_CategoryResList(int status, string notification, IEnumerable<GetCategoryRes> categoryResList)
        {
            this.Status = status;
            this.Notification = notification;
            this.CategoryResList = categoryResList;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetCategoryRes> CategoryResList { get => categoryResList; set => categoryResList = value; }
    }
}
