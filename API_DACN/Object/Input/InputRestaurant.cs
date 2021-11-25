using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputRestaurant
    {
        public string name { get; set; }
        public string userId { get; set; }
        public string line { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string longLat { get; set; }
        public string openTime { get; set; }
        public string closeTime { get; set; }
    }

    public class InputRes_distance
    {
        public float distance { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_District
    {
        public string district { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_Catelory
    {
        public string cateloryId { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_Food
    {
        public string foodId { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_Search
    {
        public List<int> catelogyList { get; set; }
        public List<string> districtList { get; set; }
        public string name { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_CategoryList
    {
        public List<int> catelogyList { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_DistrictList
    {
        public List<string> districtList { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class InputRes_CategoryListAndDistrictList
    {
        public List<int> catelogyList { get; set; }
        public List<string> districtList { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
    }
}
