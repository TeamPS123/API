using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputStatistic
    {
        public string userId { get; set; }
        public string restaurantId { get; set; }
        public string month1 { get; set; }
        public string year1 { get; set; }
        public string month2 { get; set; }
        public string year2 { get; set; }
    }

    public class InputStatisticWithYear
    {
        public string userId { get; set; }
        public string restaurantId { get; set; }
        public string year1 { get; set; }
        public string year2 { get; set; }
    }
}
