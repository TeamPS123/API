using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Update
{
    public class UpdateRestaurant
    {
        public string restaurantId { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string line { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string longLat { get; set; }
        public string openTime { get; set; }
        public string closeTime { get; set; }
        public bool status { get; set; }
        public string statusCO { get; set; }
    }
}
