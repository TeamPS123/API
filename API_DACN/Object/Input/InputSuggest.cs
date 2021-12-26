using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputSuggest
    {
        public string userId { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int distance { get; set; }
        public int rangeDay { get; set; }
    }
}
