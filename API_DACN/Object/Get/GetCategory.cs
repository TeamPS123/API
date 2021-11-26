using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Get
{
    public class GetCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool ? status { get; set; }
    }

    public class Message_Category
    {
        private int status;
        private string notification;
        private IEnumerable<GetCategory> categories;

        public Message_Category(int status, string notification, IEnumerable<GetCategory> categories)
        {
            this.status = status;
            this.notification = notification;
            this.categories = categories;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<GetCategory> Categories { get => categories; set => categories = value; }
    }
}
