using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Image
    {
        public string Link { get; set; }
        public string FoodId { get; set; }
        public string RestaurantId { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
    }
}
