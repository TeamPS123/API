using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Food
    {
        public Food()
        {
            ReserveFoods = new HashSet<ReserveFood>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public string MenuId { get; set; }
        public string CategoryId { get; set; }
        public bool? Status { get; set; }
        public string KeyWord { get; set; }

        public virtual Category Category { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<ReserveFood> ReserveFoods { get; set; }
    }
}
