using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputReserveFood
    {
        public string userId { get; set; }
        public string reserveTableId { get; set; }
        public IEnumerable<Food1> foods { get; set; }
    }

    public class Food1
    {
        public string foodId { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
