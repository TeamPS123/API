using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class ReserveFood
    {
        public string FoodId { get; set; }
        public string ReserveTable { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual Food Food { get; set; }
        public virtual ReserveTable ReserveTableNavigation { get; set; }
    }
}
