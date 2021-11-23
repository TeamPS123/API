using API_DACN.Database;
using API_DACN.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model
{
    public class RestaurantModel
    {
        private readonly food_location_dbContext db;
        private NextId setId;

        public RestaurantModel(food_location_dbContext db)
        {
            this.db = db;
            setId = new NextId(db);
        }

        //------------------------------restaurant-----------------------------------
        public string AddRestaurant(Object.Input.InputRestaurant restaurant)
        {
            string restaurantId = setId.GetRestaurantId();
            try
            {
                Restaurant r = new Restaurant();
                r.Id = restaurantId;
                r.Name = restaurant.name;
                r.UserId = restaurant.userId;
                r.Line = restaurant.line;
                r.City = restaurant.city;
                r.District = restaurant.district;
                r.LongLat = restaurant.longLat;
                r.Status = true;
                r.OpenTime = restaurant.openTime;
                r.CloseTime = restaurant.closeTime;
                db.Restaurants.Add(r);
                db.SaveChanges();
            }
            catch
            {
                return "null";
            }
            return restaurantId;
        }

        public bool updateRestaurant(Object.Update.UpdateRestaurant restaurant)
        {
            try
            {
                var data = db.Restaurants.Find(restaurant.restaurantId);
                data.Name = restaurant.name;
                data.Line = restaurant.line;
                data.City = restaurant.city;
                data.District = restaurant.district;
                data.LongLat = restaurant.longLat;
                data.OpenTime = restaurant.openTime;
                data.CloseTime = restaurant.closeTime;
                data.Status = restaurant.status;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Object.Get.GetRestaurant getRestaurant(string restaurantId)
        {
            try
            {
                var result = from a in db.Restaurants
                             where a.Id == restaurantId && a.Status == true
                             select new Object.Get.GetRestaurant()
                             {
                                 restaurantId = restaurantId,
                                 name = a.Name,
                                 line = a.Line,
                                 city = a.City,
                                 district = a.District,
                                 longLat = a.LongLat,
                                 openTime = a.OpenTime,
                                 closeTime = a.CloseTime,
                                 phoneRes = a.PhoneRestaurant,
                                 pic = (List<string>)(from b in db.Images
                                                      where b.RestaurantId == restaurantId && b.FoodId != "0"
                                                      select b.Link),
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

                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Object.Get.GetRestaurant getResWithPromotion(string promotionId)
        {
            try
            {
                var result = from a in db.Promotions
                             where a.Id == promotionId && a.Restaurant.Status == true
                             select new Object.Get.GetRestaurant()
                             {
                                 restaurantId = a.RestaurantId,
                                 name = a.Restaurant.Name,
                                 line = a.Restaurant.Line,
                                 city = a.Restaurant.City,
                                 district = a.Restaurant.District,
                                 longLat = a.Restaurant.LongLat,
                                 openTime = a.Restaurant.OpenTime,
                                 closeTime = a.Restaurant.CloseTime,
                                 phoneRes = a.Restaurant.PhoneRestaurant,
                                 pic = (List<string>)(from b in db.Images
                                                      where b.RestaurantId == a.Restaurant.Id && b.FoodId != "0"
                                                      select b.Link),
                                 categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.RestaurantId).Select(c => c.Category.Name).ToList()),
                                 promotionRes = from c in db.Promotions
                                                where c.RestaurantId == a.RestaurantId
                                                select new Object.Get.GetPromotion_Res()
                                                {
                                                    id = c.Id,
                                                    name = c.Name,
                                                    info = c.Info,
                                                    value = c.Value
                                                },
                                 categoryRes = from b in db.RestaurantDetails
                                               where b.RestaurantId == a.RestaurantId
                                               select new Object.Get.GetCategoryRes()
                                               {
                                                   id = b.CategoryId,
                                                   name = b.Category.Name,
                                                   icon = b.Category.icon
                                               }
                             };

                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetRestaurant> restaurantList()
        {
            try
            {
                var result = from a in db.Restaurants
                             where a.Status == true
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
                                 phoneRes = a.PhoneRestaurant,
                                 pic = (List<string>)(from b in db.Images
                                                      where b.RestaurantId == a.Id && b.FoodId != "0"
                                                      select b.Link),
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

                return result;
            }
            catch
            {
                return null;
            }
        }

        //------------------------------menu-----------------------------------
        public string CreateMenu(Object.Input.InputMenu menu)
        {
            string menuId = setId.GetMenuId();
            try
            {
                Menu m = new Menu()
                {
                    Id = menuId,
                    RestaurantId = menu.restaurantId,
                    Name = menu.name,
                };
                db.Menus.Add(m);
                db.SaveChanges();
            }
            catch
            {
                return "null";
            }
            return menuId;
        }

        public bool updateMenu(Object.Update.UpdateMenu menu)
        {
            try
            {
                var data = db.Menus.Find(menu.menuId);
                data.Name = menu.name;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Object.Get.GetMenu getMenu(string menuId)
        {
            try
            {
                var result = from a in db.Menus
                             where a.Id == menuId
                             select new Object.Get.GetMenu()
                             {
                                 menuId = a.Id,
                                 name = a.Name,
                                 foodList = from b in a.Foods
                                         where b.MenuId == menuId
                                         select new Object.Get.FoodOfMenu()
                                         {
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             pic = (List<string>)(from c in db.Images
                                                                  where c.FoodId == b.Id
                                                                  select c.Link)
                                         }
                             };

                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetMenu> menuList()
        {
            try
            {
                var result = from a in db.Menus
                             select new Object.Get.GetMenu()
                             {
                                 menuId = a.Id,
                                 name = a.Name,
                                 foodList = from b in a.Foods
                                         where b.MenuId == a.Id
                                         select new Object.Get.FoodOfMenu()
                                         {
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             pic = (List<string>)(from c in db.Images
                                                                  where c.FoodId == b.Id
                                                                  select c.Link)
                                         }
                             };

                return result;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetMenu> menuListWithRes(string restaurantId)
        {
            try
            {
                var result = from a in db.Menus
                             where a.RestaurantId == restaurantId
                             select new Object.Get.GetMenu()
                             {
                                 menuId = a.Id,
                                 name = a.Name,
                                 foodList = from b in a.Foods
                                         where b.MenuId == a.Id
                                         select new Object.Get.FoodOfMenu()
                                         {
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             pic = (List<string>)(from c in db.Images
                                                                  where c.FoodId == b.Id
                                                                  select c.Link)
                                         }
                             };

                return result;
            }
            catch
            {
                return null;
            }
        }

        //------------------------------category-----------------------------------
        public string CreateCategory(Object.Input.InputCategory category)
        {
            string categoryId = setId.GetCategoryId();
            try
            {
                Category c = new Category();
                c.Id = categoryId;
                c.Name = category.name;
                db.Categories.Add(c);
                db.SaveChanges();
            }
            catch
            {
                return "null";
            }
            return categoryId;
        }

        //----------------------------categoryRES----------------------------------
        public IEnumerable<Object.Get.GetCategoryRes> categoryList()
        {
            try
            {
                var result = from a in db.CategoryRestaurants
                             select new Object.Get.GetCategoryRes()
                             {
                                 id = a.Id,
                                 name = a.Name,
                                 icon = a.icon,
                             };

                return result;
            }
            catch
            {
                return null;
            }
        }

        //------------------------------food-----------------------------------
        public bool AddFood(Object.Input.InputFood updateMenu)
        {
            try
            {
                foreach (var item in updateMenu.foods)
                {
                    Food food = new Food
                    {
                        Id = setId.GetFoodId(),
                        Name = item.name,
                        Price = item.price,
                        Unit = item.unit,
                        MenuId = updateMenu.menuId,
                        CategoryId = item.categoryId,
                    };
                    db.Foods.Add(food);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool updateFood(Object.Update.UpdateFood food)
        {
            try
            {
                foreach (var item in food.foods)
                {
                    var data = db.Foods.Find(item.foodId);
                    data.Name = item.name;
                    data.Price = item.price;
                    data.Unit = item.unit;
                    data.CategoryId = item.categoryId;
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Object.Get.GetFood getFood(string foodId){
            try
            {
                var result = from a in db.Foods
                             where a.Id == foodId
                             select new Object.Get.GetFood()
                             {
                                 name = a.Name,
                                 price = a.Price,
                                 unit = a.Unit,
                                 menuName = a.Menu.Name,
                                 categoryName = a.Category.Name,
                                 pic = (List<string>)(from c in db.Images
                                                      where c.FoodId == foodId
                                                      select c.Link)
                             };

                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetFood> foodList()
        {
            try
            {
                var result = from a in db.Foods
                             select new Object.Get.GetFood()
                             {
                                 foodId = a.Id,
                                 name = a.Name,
                                 price = a.Price,
                                 unit = a.Unit,
                                 menuName = a.Menu.Name,
                                 categoryName = a.Category.Name,
                                 pic = (List<string>)(from c in db.Images
                                                      where c.FoodId == a.Id
                                                      select c.Link)
                             };

                return result;
            }
            catch
            {
                return null;
            }
        }

        //------------------------------promotion-----------------------------------
        public string CreatePromotion(Object.Input.InputPromotion promotion)
        {
            string promotionId = setId.GetPromotionId();
            try
            {
                Promotion p = new Promotion()
                {
                    Id = promotionId,
                    RestaurantId = promotion.restaurantId,
                    Name = promotion.name,
                    Info = promotion.info,
                    Value = promotion.value,
                };
                db.Promotions.Add(p);
                db.SaveChanges();
            }
            catch
            {
                return "null";
            }
            return promotionId;
        }

        public bool UpdatePromotion(Object.Update.UpdatePromotion promotion)
        {
            try
            {
                var data = db.Promotions.Find(promotion.promotionId);
                data.Name = promotion.name;
                data.Info = promotion.info;
                data.Value = promotion.value;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Object.Get.GetPromotion> promotionList(LngLat lngLat)
        {
            try
            {
                return from a in db.Promotions
                       select new Object.Get.GetPromotion()
                       {
                           promotionId = a.Id,
                           restaurantName = a.Restaurant.Name,
                           name = a.Name,
                           info = a.Info,
                           value = a.Value,
                           line = a.Restaurant.Line,
                           district = a.Restaurant.District,
                           city = a.Restaurant.City,
                           image = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                           distance = Distance.distance(a.Restaurant.LongLat, lngLat),
                       };
            }
            catch
            {
                return null;
            }
        }
    }
}