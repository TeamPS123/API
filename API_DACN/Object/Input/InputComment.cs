using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputComment
    {
        public string content { get; set; }
        public int value { get; set; }
        public string userId { get; set; }
        public string RestaurantId { get; set; }
        public string date { get; set; }
    }
}
