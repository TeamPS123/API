using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class User
    {
        public User()
        {
            Rates = new HashSet<Rate>();
            ReserveTables = new HashSet<ReserveTable>();
            Restaurants = new HashSet<Restaurant>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassswordHash { get; set; }
        public bool IsBusiness { get; set; }
        public bool? Gender { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<ReserveTable> ReserveTables { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
