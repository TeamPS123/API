using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object
{
    public class UserSignUp
    {
        public string fullName { get; set; }
        public string phone { get; set; }
        public string pass { get; set; }
        public bool business { get; set; }
        public bool? gender { get; set; }
    }
}
