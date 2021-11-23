using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class ReserveTable
    {
        public ReserveTable()
        {
            ReserveFoods = new HashSet<ReserveFood>();
        }

        public string Id { get; set; }
        public int QuantityPeople { get; set; }
        public string Time { get; set; }
        public int Status { get; set; }
        public string RestaurantId { get; set; }
        public string PromotionId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ReserveFood> ReserveFoods { get; set; }
    }
}
