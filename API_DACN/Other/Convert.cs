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

            foreach (var item in strList)
            {
                str = str + item + ", ";
            }

            if (str.Length > 2)
            {
                str = str.Remove(str.Length - 2);
            }

            return str;
        }

        public static string ConvertDateTimeToString(DateTime dt){
            //chuyển đổi datetime qua string
            string temp = dt + "";
            //tách bỏ thời giản lấy ngày
            string[] temps = temp.Split(" ");
            //tách ngày tháng năm ra riêng biệt
            string[] time = temps[0].Split("/");
            //chuyển ngày đúng định dạng
            if (int.Parse(time[1]) < 10)
            {
                time[1] = "0" + time[1];
            }
            //ghép lại giống định dạng sql
            string day = time[2] + "-" + time[0] + "-" + time[1];
            //đưa vào danh sách cho model
            return day;
        }

        public static string ConvertDateTimeToString_FromClient(string dt)
        {
            //chuyển đổi datetime qua string
            string temp = dt + "";
            //tách bỏ thời giản lấy ngày
            string[] temps = temp.Split(",");
            return temps[1];
        }
    }
}
