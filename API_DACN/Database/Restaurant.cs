using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Menus = new HashSet<Menu>();
            Promotions = new HashSet<Promotion>();
            RestaurantDetails = new HashSet<RestaurantDetail>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string LongLat { get; set; }
        public bool Status { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<RestaurantDetail> RestaurantDetails { get; set; }
    }
}
