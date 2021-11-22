using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Menu
    {
        public Menu()
        {
            Foods = new HashSet<Food>();
        }

        public string Id { get; set; }
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public bool ? status { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
