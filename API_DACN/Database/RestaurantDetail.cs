using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class RestaurantDetail
    {
        public string RestaurantId { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryRestaurant Category { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
