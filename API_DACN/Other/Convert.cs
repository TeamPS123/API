using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class Convert
    {
        public static string ConvertListToString(List<string> strList)
        {
            string str = "";

            foreach(var item in strList)
            {
                str = str + item + ", ";
            }

            if(str.Length > 2)
            {
                str = str.Remove(str.Length - 2);
            }

            return str;
        }
    }
}
