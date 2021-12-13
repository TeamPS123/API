using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class CategoryRestaurant
    {
        public CategoryRestaurant()
        {
            RestaurantDetails = new HashSet<RestaurantDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<RestaurantDetail> RestaurantDetails { get; set; }
    }
}
