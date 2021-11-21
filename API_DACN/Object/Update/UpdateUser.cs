using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Update
{
    public class UpdateUser
    {
        public string userId { get; set; }
        public string fullName { get; set; }
        public string pass { get; set; }
        public bool isBusiness { get; set; }
        public bool? gender { get; set; }
    }
}
