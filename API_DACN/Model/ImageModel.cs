using API_DACN.Database;
using API_DACN.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model
{
    public class ImageModel
    {
        private readonly food_location_dbContext db;
        private NextId setId;

        public ImageModel(food_location_dbContext db)
        {
            this.db = db;
            setId = new NextId(db);
        }

        //public void AddImage(string filename, string foodId, string restaurantId, string userId, string categoryId)
        //{
        //    Image image = new Image()
        //    {
        //        Link = "https://ps.covid21tsp.space/Picture/" + filename,
        //        FoodId = foodId,
        //        RestaurantId = restaurantId,
        //        UserId = userId,
        //        CategoryId = categoryId,
        //    };
        //    db.Images.Add(image);
        //    db.SaveChanges();
        //}

        public bool AddImageOfUser(string filename, string userId)
        {
            try
            {
                Image image = new Image()
                {
                    Link = "https://ps.covid21tsp.space/Picture/" + filename,
                    FoodId = "0",
                    RestaurantId = "0",
                    UserId = userId,
                    CategoryId = "0",
                };
                db.Images.Add(image);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AddImageOfRes(string filename, string userId, string restaurantId)
        {
            try
            {
                Image image = new Image()
                {
                    Link = "https://ps.covid21tsp.space/Picture/" + filename,
                    FoodId = "0",
                    RestaurantId = restaurantId,
                    UserId = userId,
                    CategoryId = "0",
                };
                db.Images.Add(image);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AddImageOfFood(string filename, string userId, string restaurantId, string foodId)
        {
            try
            {
                Image image = new Image()
                {
                    Link = "https://ps.covid21tsp.space/Picture/" + filename,
                    FoodId = foodId,
                    RestaurantId = restaurantId,
                    UserId = userId,
                    CategoryId = "0",
                };
                db.Images.Add(image);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
