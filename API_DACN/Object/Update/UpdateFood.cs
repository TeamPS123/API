using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Update
{
    public class Food
    {
        public string foodId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string unit { get; set; }
        public string categoryId { get; set; }
    }

    public class UpdateFood
    {
        public string userId { get; set; }
        public IEnumerable<Food> foods { get; set; }
    }
}
