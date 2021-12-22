using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public static class Date
    {
        public static bool checkDate(string date, string day, string month, string year)
        {
            string[] time = date.Split("-");
            if (!year.Equals(time[0]))
            {
                return false;
            }
            if (!month.Equals("0"))
            {
                if(int.Parse(month) < 10)
                {
                    month = "0" + month;
                }
                if (!month.Equals(time[1]))
                {
                    return false;
                }
                if (!day.Equals("0"))
                {
                    if (!day.Equals(time[2]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
