using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Rate
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public string Date { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
