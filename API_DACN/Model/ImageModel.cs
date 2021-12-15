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
        private string domain = "https://ps.covid21tsp.space/Picture/"; 

        public ImageModel(food_location_dbContext db)
        {
            this.db = db;
            setId = new NextId(db);
            domain = "https://ps.covid21tsp.space/Picture/";
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

        public bool AddImageOfUser(string filename, string userId, string path)
        {
            try
            {
                Image image = new Image()
                {
                    Link = domain + filename,
                    FoodId = "0",
                    RestaurantId = "0",
                    UserId = userId,
                    CategoryId = "0",
                    Path = path,
                    ReviewId = 0,
                    RateId = 0,
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

        public bool AddImageOfRes(string filename, string userId, string restaurantId, string path)
        {
            try
            {
                Image image = new Image()
                {
                    Link = domain + filename,
                    FoodId = "0",
                    RestaurantId = restaurantId,
                    UserId = userId,
                    CategoryId = "0",
                    Path = path,
                    ReviewId = 0,
                    RateId = 0,
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

        public string AddImageOfFood(string filename, string userId, string restaurantId, string foodId, string path)
        {
            try
            {
                string link = domain + filename;
                Image image = new Image()
                {
                    Link = link,
                    FoodId = foodId,
                    RestaurantId = restaurantId,
                    UserId = userId,
                    CategoryId = "0",
                    Path = path,
                    ReviewId = 0,
                    RateId = 0,
                };
                db.Images.Add(image);
                db.SaveChanges();

                return link;
            }
            catch
            {
                return "null";
            }
        }

        public List<string> DelImageOfFood(string foodId)
        {
            List<string> ImgList = null;

            try
            {
                IEnumerable<Image> images = db.Images.Where(t => t.FoodId == foodId);
                foreach(var item in images)
                {
                    db.Images.Remove(item);
                    db.SaveChanges();
                    ImgList.Add(item.Link);
                }
            }
            catch
            {

            }

            return ImgList;
        }
    }
}
