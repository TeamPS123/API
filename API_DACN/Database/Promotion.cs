using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Promotion
    {
        public Promotion()
        {
            ReserveTables = new HashSet<ReserveTable>();
        }

        public string Id { get; set; }
        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Value { get; set; }
        public bool? Status { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<ReserveTable> ReserveTables { get; set; }
    }
}
