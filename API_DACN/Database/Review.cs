using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public int Value { get; set; }
        public string Date { get; set; }
        public long? CountLike { get; set; }
    }
}
