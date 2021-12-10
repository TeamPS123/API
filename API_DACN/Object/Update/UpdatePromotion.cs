using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Update
{
    public class UpdatePromotion
    {
        public string promotionId { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string value { get; set; }
        public string userId { get; set; }
        public bool status { get; set; }
    }
}
