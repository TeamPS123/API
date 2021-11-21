using API_DACN.Database;
using API_DACN.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model
{
    public class SearchModel
    {
        private readonly food_location_dbContext db;

        public SearchModel(food_location_dbContext db)
        {
            this.db = db;
        }

        //Restaurant list distance <= 5km
        public IEnumerable<Object.Get.GetRestaurant> resList(LngLat lngLat, float dis)
        {
            try
            {
                List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();
                var data = db.Restaurants.Where(t => t.Status == true);

                foreach(var item in data)
                {
                    double distance1 = Distance.distance(item.LongLat, lngLat);
                    if (distance1 <= dis)
                    {
                        restaurants.Add(objectRes(item, distance1.ToString()));
                    }
                }

                return restaurants;
            }
            catch
            {
                return null;
            }
        }

        //Restaurant list with district
        public IEnumerable<Object.Get.GetRestaurant> resListWithDistrict(string district, LngLat lngLat)
        {
            try
            {
                if(lngLat.Latitude == 0)
                {
                    return from a in db.Restaurants
                           where a.District == district && a.Status == true
                           select new Object.Get.GetRestaurant()
                            {
                               restaurantId = a.Id,
                               name = a.Name,
                                line = a.Line,
                                city = a.City,
                                district = a.District,
                                longLat = a.LongLat,
                                openTime = a.OpenTime,
                                closeTime = a.CloseTime,
                                distance = "Không xác định",
                                pic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).ToList(),
                                categoryRes = (List<string>)a.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name),
                           }; 
                }
                List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();
                var data = db.Restaurants.Where(t => t.District == district);

                foreach (var item in data)
                {
                    double distance1 = Distance.distance(item.LongLat, lngLat);
                    restaurants.Add(objectRes(item, distance1.ToString()));
                }

                return restaurants;
            }
            catch
            {
                return null;
            }
        }

        //Restaurant list with category and food
        public IEnumerable<Object.Get.GetRestaurant> resListSearch(string name, LngLat lngLat)
        {
            try
            {
                //var result = db.Product.Where(p => p.Productname.ToUpper().Contains(key.ToUpper())).ToList();

                // Search category name. if data is null, will search food name
                var data = from b in db.Foods
                            where b.Category.Name.ToUpper().Contains(name.ToUpper()) && b.Menu.Restaurant.Status == true
                           select b.Menu.Restaurant;

                if(data.Count() == 0)
                {
                    data = from c in db.Foods
                           where c.Name.ToUpper().Contains(name.ToUpper()) && c.Menu.Restaurant.Status == true
                           select c.Menu.Restaurant;
                }

                if(data.Count() == 0)
                {
                    return null;
                }
                else if(data.Count() > 1)
                {

                    data = from m in data
                           group m by m.Id into g
                           select g.FirstOrDefault();
                }

                //// Search catelory name and food name
                //var data1 = from b in db.Foods
                //            where b.Category.Name.ToUpper().Contains(name.ToUpper())
                //            select b.Menu.Restaurant;
                //var data2 = from c in db.Foods
                //            where c.Name.ToUpper().Contains(name.ToUpper())
                //            select c.Menu.Restaurant;
                //List<Database.Restaurant> data_combine = new List<Restaurant>();
                //foreach(var item in data1)
                //{
                //    data_combine.Add(item);
                //}
                //foreach (var item in data2)
                //{
                //    data_combine.Add(item);
                //}

                if (lngLat.Latitude == 0)
                {                               
                    return from a in data
                           select new Object.Get.GetRestaurant()
                           {
                               restaurantId = a.Id,
                               name = a.Name,
                               line = a.Line,
                               city = a.City,
                               district = a.District,
                               longLat = a.LongLat,
                               openTime = a.OpenTime,
                               closeTime = a.CloseTime,
                               distance = "Không xác định",
                               pic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).ToList(),
                               categoryRes = (List<string>)a.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name),
                           };
                }
                List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();

                foreach (var item in data)
                {
                    double distance1 = Distance.distance(item.LongLat, lngLat);
                    restaurants.Add(objectRes(item, distance1.ToString()));
                }

                return restaurants;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        //Restaurant list with food
        //public IEnumerable<Object.Get.GetRestaurant> resListWithFood(string foodId, LngLat lngLat)
        //{
        //    try
        //    {
        //        var data = from b in db.Foods
        //                   where b.Id == foodId
        //                   select b.Menu.Restaurant;

        //        data = from m in data
        //               group m by m.Id into g
        //               select g.FirstOrDefault();

        //        if (lngLat.Latitude == 0)
        //        {
        //            return from a in data
        //                   select new Object.Get.GetRestaurant()
        //                   {
        //                       restaurantId = a.Id,
        //                       name = a.Name,
        //                       line = a.Line,
        //                       city = a.City,
        //                       district = a.District,
        //                       longLat = a.LongLat,
        //                       openTime = a.OpenTime,
        //                       closeTime = a.CloseTime,
        //                       distance = "Không xác định",
        //                       pic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).ToList()
        //                   };
        //        }
        //        List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();

        //        foreach (var item in data)
        //        {
        //            double distance1 = distance(item.LongLat, lngLat);
        //            restaurants.Add(objectRes(item, distance1.ToString()));
        //        }

        //        return restaurants;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        // Distance two marker

        // Create object Api
        private Object.Get.GetRestaurant objectRes(Database.Restaurant item, string distance)
        {
            return new Object.Get.GetRestaurant()
            {
                restaurantId = item.Id,
                name = item.Name,
                line = item.Line,
                city = item.City,
                district = item.District,
                longLat = item.LongLat,
                openTime = item.OpenTime,
                closeTime = item.CloseTime,
                distance = distance,
                pic = db.Images.Where(t => t.RestaurantId == item.Id && t.FoodId == "0").Select(c => c.Link).ToList(),
                categoryRes = (List<string>)item.RestaurantDetails.Where(t => t.RestaurantId == item.Id).Select(c => c.Category.Name),
            };
        }
    }
}
