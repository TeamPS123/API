using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{

    public class Distance
    {
        private static double CalculationByDistance(LngLat StartP, LngLat EndP)   // return km
        {
            int Radius = 6371;// radius of earth in Km
            double lat1 = StartP.Latitude;
            double lat2 = EndP.Latitude;
            double lon1 = StartP.Longitude;
            double lon2 = EndP.Longitude;
            double dLat = ConvertToRadians(lat2 - lat1);
            double dLon = ConvertToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                    + Math.Cos(ConvertToRadians(lat1))
                    * Math.Cos(ConvertToRadians(lat2)) * Math.Sin(dLon / 2)
                    * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            double km = Radius * c;
            DecimalFormat newFormat = new DecimalFormat("####");
            int kmInDec = int.Parse(newFormat.Format(km));
            //double meter = valueResult * 1000;
            //int meterInDec = int.Parse(newFormat.Format(meter));

            return km;
        }

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double distance(string LongLat_Res, LngLat lngLat)
        {
            string[] longLat = LongLat_Res.Split(",");
            LngLat lngLat1 = new LngLat(double.Parse(longLat[0]), double.Parse(longLat[1]));
            return CalculationByDistance(lngLat, lngLat1);
        }
    }
    public class LngLat
    {
        private double latitude;
        private double longitude;

        public LngLat(double longitude, double latitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
    }
}
