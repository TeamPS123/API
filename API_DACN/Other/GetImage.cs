using API_DACN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class GetImage
    {
        public static List<string> getImageWithRes(string restaurantId, food_location_dbContext db)
        {
            //take food list of restaurant
            var foodList = from a in db.Foods
                           where a.Menu.RestaurantId == restaurantId
                           select a.Id;

            List<string> imgList = new List<string>();

            //take img first of food
            foreach (var item in foodList)
            {
                string img = db.Images.Where(t => t.FoodId == item && t.RestaurantId == restaurantId).Select(c => c.Link).FirstOrDefault();

                if (img != null) { 
                    imgList.Add(img); 
                }
            }

            return imgList;
        }
    }
}
