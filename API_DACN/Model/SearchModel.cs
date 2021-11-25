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
                               phoneRes = a.PhoneRestaurant,
                               mainPic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                               pic = GetImage.getImageWithRes(a.Id, db),
                               categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name).ToList()),
                               promotionRes = from c in db.Promotions
                                              where c.RestaurantId == a.Id
                                              select new Object.Get.GetPromotion_Res()
                                              {
                                                  id = c.Id,
                                                  name = c.Name,
                                                  info = c.Info,
                                                  value = c.Value
                                              },
                                categoryRes = from b in db.RestaurantDetails
                                             where b.RestaurantId == a.Id
                                             select new Object.Get.GetCategoryRes()
                                             {
                                                 id = b.CategoryId,
                                                 name = b.Category.Name,
                                                 icon = b.Category.icon
                                             }
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

                if(data.Count() > 1)
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
                               phoneRes = a.PhoneRestaurant,
                               mainPic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                               pic = GetImage.getImageWithRes(a.Id, db),
                               categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name).ToList()),
                               promotionRes = from c in db.Promotions
                                              where c.RestaurantId == a.Id
                                              select new Object.Get.GetPromotion_Res()
                                              {
                                                  id = c.Id,
                                                  name = c.Name,
                                                  info = c.Info,
                                                  value = c.Value
                                              },
                               categoryRes = from b in db.RestaurantDetails
                                             where b.RestaurantId == a.Id
                                             select new Object.Get.GetCategoryRes()
                                             {
                                                 id = b.CategoryId,
                                                 name = b.Category.Name,
                                                 icon = b.Category.icon
                                             }
                           };
                }
                List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();

                foreach (var item in data)
                {
                    double distance1 = Distance.distance(item.LongLat, lngLat);
                    if(distance1 <= 5)
                    {
                        restaurants.Add(objectRes(item, distance1.ToString()));
                    }
                }

                return restaurants;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        //Restaurant list with category and food
        public IEnumerable<Object.Get.GetRestaurant> resListSearch1(List<int> catelogyList, List<string> districtList, string name, LngLat lngLat)
        {
            try
            {
                IEnumerable<Restaurant> data = new List<Restaurant>();

                if (name != string.Empty)
                {
                    // Search category name. if data is null, will search food name
                    data = from b in db.Foods
                           where b.Category.Name.ToUpper().Contains(name.ToUpper()) && b.Menu.Restaurant.Status == true
                           select b.Menu.Restaurant;

                    if (data.Count() == 0)
                    {
                        data = from c in db.Foods
                               where c.Name.ToUpper().Contains(name.ToUpper()) && c.Menu.Restaurant.Status == true
                               select c.Menu.Restaurant;
                    }

                    if (catelogyList.Count() > 0)
                    {
                        var resDetails = from a in db.RestaurantDetails
                                         where data.Select(t => t.Id).Contains(a.RestaurantId) == true
                                         select a;

                        //Search by categoryResList when name is not null
                        data = from a in resDetails
                               where catelogyList.Contains(a.CategoryId) == true
                               select a.Restaurant;
                    }
                }
                else if (catelogyList.Count() > 0)
                {
                    //Search by categoryResList when name is null
                    data = from a in db.RestaurantDetails
                           where a.Restaurant.Status == true && catelogyList.Contains(a.CategoryId) == true
                           select a.Restaurant;
                }

                if (data.Count() < 1)
                {
                    if (districtList.Count() > 0)
                    {
                        //Search by districtList when data is null
                        data = from a in db.Restaurants
                               where a.Status == true && districtList.Contains(a.District) == true
                               select a;
                    }
                }
                else
                {
                    if (districtList.Count() > 0)
                    {
                        //Search by districtList when data is not null
                        data = from a in data
                               where districtList.Contains(a.District) == true
                               select a;
                    }
                }

                if (data.Count() > 1)
                {
                    data = from m in data
                           group m by m.Id into g
                           select g.FirstOrDefault();
                }

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
                               phoneRes = a.PhoneRestaurant,
                               mainPic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                               pic = GetImage.getImageWithRes(a.Id, db),
                               categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name).ToList()),
                               promotionRes = from c in db.Promotions
                                              where c.RestaurantId == a.Id
                                              select new Object.Get.GetPromotion_Res()
                                              {
                                                  id = c.Id,
                                                  name = c.Name,
                                                  info = c.Info,
                                                  value = c.Value
                                              },
                               categoryRes = from b in db.RestaurantDetails
                                             where b.RestaurantId == a.Id
                                             select new Object.Get.GetCategoryRes()
                                             {
                                                 id = b.CategoryId,
                                                 name = b.Category.Name,
                                                 icon = b.Category.icon
                                             }
                           };
                }
                List<Object.Get.GetRestaurant> restaurants = new List<Object.Get.GetRestaurant>();

                foreach (var item in data)
                {
                    double distance1 = Distance.distance(item.LongLat, lngLat);
                    if (distance1 <= 5)
                    {
                        restaurants.Add(objectRes(item, distance1.ToString()));
                    }
                }

                return restaurants;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Restaurant list with categoryRes List
        public IEnumerable<Object.Get.GetRestaurant> resResWithCategorys(List<int> categoryResList, LngLat lngLat)
        {
            try
            {
                List<Object.Get.GetRestaurant> restaurantList = new List<Object.Get.GetRestaurant>();

                var result = from a in db.RestaurantDetails
                                where a.Restaurant.Status == true && categoryResList.Contains(a.CategoryId) == true 
                                select a.Restaurant;

                foreach (var item1 in result)
                {
                    double distance1 = Distance.distance(item1.LongLat, lngLat);
                    restaurantList.Add(objectRes(item1, distance1.ToString()));
                }

                var t = from a in restaurantList
                        group a by a.restaurantId into g
                        select g.FirstOrDefault();

                return t;
            }
            catch(Exception ex)
            {
                string i = ex.Message;
                return null;
            }
        }

        //Restaurant list with district List
        public IEnumerable<Object.Get.GetRestaurant> resResWithDistricts(List<string> districtList, LngLat lngLat)
        {
            try
            {
                List<Object.Get.GetRestaurant> restaurantList = new List<Object.Get.GetRestaurant>();

                var result = from a in db.Restaurants
                                where a.Status == true && districtList.Contains(a.District) == true
                                select a;

                foreach (var item1 in result)
                {
                    double distance1 = Distance.distance(item1.LongLat, lngLat);
                    restaurantList.Add(objectRes(item1, distance1.ToString()));
                }

                var t = from a in restaurantList
                        group a by a.restaurantId into g
                        select g.FirstOrDefault();

                return t;
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return null;
            }
        }

        //Restaurant list with categoryRes List and district List
        public IEnumerable<Object.Get.GetRestaurant> getResWithCategorysAndDistricts(List<int> categoryResList, List<string> districtList, LngLat lngLat)
        {
            try
            {
                List<Object.Get.GetRestaurant> restaurantList = new List<Object.Get.GetRestaurant>();

                var result = from a in db.RestaurantDetails
                                      where a.Restaurant.Status == true && categoryResList.Contains(a.CategoryId) == true
                                      select a.Restaurant;

                foreach (var item1 in result)
                {
                    double distance1 = Distance.distance(item1.LongLat, lngLat);
                    restaurantList.Add(objectRes(item1, distance1.ToString()));
                }

                var h = from a in restaurantList
                        where districtList.Contains(a.district) == true
                        select a;

                var t = from a in h
                        group a by a.restaurantId into g
                        select g.FirstOrDefault();

                return t;
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return null;
            }
        }

        //catelory list with GetRestaurant
        public IEnumerable<Object.Get.GetCategoryRes> categoryResList(IEnumerable<Object.Get.GetRestaurant> restaurants)
        {
            List<Object.Get.GetCategoryRes> categoryRes = new List<Object.Get.GetCategoryRes>();
            foreach(var item in restaurants)
            {
                foreach(var item1 in item.categoryRes)
                {
                    categoryRes.Add(item1);
                }
            }

            return from a in categoryRes
                   group a by a.id into g
                   select g.FirstOrDefault();
        }

        //district list with GetRestaurant
        public IEnumerable<string> districtList(IEnumerable<Object.Get.GetRestaurant> restaurants)
        {
            return from a in restaurants
                   group a by a.district into g
                   select g.FirstOrDefault().district;
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
                phoneRes = item.PhoneRestaurant,
                mainPic = db.Images.Where(t => t.RestaurantId == item.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                pic = GetImage.getImageWithRes(item.Id, db),
                categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == item.Id).Select(c => c.Category.Name).ToList()),
                promotionRes = from c in db.Promotions
                               where c.RestaurantId == item.Id
                               select new Object.Get.GetPromotion_Res()
                               {
                                   id = c.Id,
                                   name = c.Name,
                                   info = c.Info,
                                   value = c.Value
                               },
                categoryRes = from b in db.RestaurantDetails
                              where b.RestaurantId == item.Id
                              select new Object.Get.GetCategoryRes()
                              {
                                  id = b.CategoryId,
                                  name = b.Category.Name,
                                  icon = b.Category.icon
                              }
            };
        }
    }
}
