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

        public IEnumerable<Object.Get.GetCategoryRes> getCategoryRes()
        {
            try
            {
                return from a in db.CategoryRestaurants
                       select new Object.Get.GetCategoryRes()
                       {
                           id = a.Id,
                           name = a.Name,
                           icon = a.icon,
                       };
            }
            catch
            {
                return null;
            }
        }


        //------------------------------restaurant-----------------------------------
        public Object.Get.GetStaticRes getStaticRes(string restaurantId)
        {
            try
            {
                return (from a in db.Restaurants
                       where a.Id == restaurantId
                       select new Object.Get.GetStaticRes()
                       {
                           amount_today = "5",
                           amount_toweek = "20",
                           statusRes = a.Status
                       }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Object.Get.getInfoRes getInfoRes(string restaurantId)
        {
            try
            {
                return (from a in db.Restaurants
                       where a.Id == restaurantId
                       select new Object.Get.getInfoRes()
                       {
                           name = a.Name,
                           pic = db.Images.Where(t => t.RestaurantId == restaurantId && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                       }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public string retaurantId(string userId)
        {
            try
            {
                return (from a in db.Restaurants
                        where a.User.Id == userId
                        select a.Id).FirstOrDefault();
            }
            catch
            {
                return "null";
            }
        }

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
                r.PhoneRestaurant = restaurant.phone;
                db.Restaurants.Add(r);
                db.SaveChanges();

                RestaurantDetail restaurantDetail = new RestaurantDetail();
                restaurantDetail.CategoryId = restaurant.categoryResId;
                restaurantDetail.RestaurantId = restaurantId;
                db.RestaurantDetails.Add(restaurantDetail);
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
                                 userId = a.UserId,
                                 restaurantId = restaurantId,
                                 name = a.Name,
                                 line = a.Line,
                                 city = a.City,
                                 district = a.District,
                                 longLat = a.LongLat,
                                 openTime = a.OpenTime,
                                 closeTime = a.CloseTime,
                                 phoneRes = a.PhoneRestaurant,
                                 mainPic = db.Images.Where(t => t.RestaurantId == restaurantId && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                 pic = GetImage.getImageWithRes(restaurantId, db),
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
                                 userId = a.Restaurant.UserId,
                                 restaurantId = a.RestaurantId,
                                 name = a.Restaurant.Name,
                                 line = a.Restaurant.Line,
                                 city = a.Restaurant.City,
                                 district = a.Restaurant.District,
                                 longLat = a.Restaurant.LongLat,
                                 openTime = a.Restaurant.OpenTime,
                                 closeTime = a.Restaurant.CloseTime,
                                 phoneRes = a.Restaurant.PhoneRestaurant,
                                 mainPic = db.Images.Where(t => t.RestaurantId == a.RestaurantId && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                 pic = GetImage.getImageWithRes(a.RestaurantId, db),
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
                                 userId = a.UserId,
                                 restaurantId = a.Id,
                                 name = a.Name,
                                 line = a.Line,
                                 city = a.City,
                                 district = a.District,
                                 longLat = a.LongLat,
                                 openTime = a.OpenTime,
                                 closeTime = a.CloseTime,
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

                return result;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetReserveTable1> getAllReserverTableByRestaurantId(string restaurantId, int code)
        {
            return from reserveTable in db.ReserveTables
                   where reserveTable.RestaurantId == restaurantId && reserveTable.Status == code
                   select new Object.Get.GetReserveTable1()
                   {
                       restaurantId = reserveTable.RestaurantId,
                       quantity = reserveTable.QuantityPeople,
                       time = reserveTable.Time,
                       reserveTableId = reserveTable.Id,
                       promotionId = reserveTable.PromotionId,
                       name = reserveTable.Name,
                       phone = reserveTable.PhoneNumber,
                       note = reserveTable.note,
                       userId = reserveTable.UserId
                   };
        }

        public string getQuantityReserveTable(string restaurantId, int code)
        {
            try { 
                return (from reserveTable in db.ReserveTables
                        where reserveTable.RestaurantId == restaurantId && reserveTable.Status == code
                        select reserveTable).Count().ToString();
            }
            catch
            {
                return "null";
            }     
        }

        public bool updateReserveTable(string reserveTableId, int code)
        {
            try
            {
                var reserveTable = db.ReserveTables.Find(reserveTableId);

                reserveTable.Status = code;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
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
                                             menuId = a.Id,
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             unit = b.Unit,
                                             categoryName = b.Category.Name,
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
                                             menuId = a.Id,
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             unit = b.Unit,
                                             categoryName = b.Category.Name,
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
                                             menuId = a.Id,
                                             foodId = b.Id,
                                             name = b.Name,
                                             price = b.Price,
                                             unit = b.Unit,
                                             categoryName = b.Category.Name,
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
                c.status = true;
                db.Categories.Add(c);
                db.SaveChanges();
            }
            catch
            {
                return "null";
            }
            return categoryId;
        }

        public IEnumerable<Object.Get.GetCategory> getAllCatelogy(string restaurantId)
        {
            try
            {
                return from a in db.Categories
                       select new Object.Get.GetCategory()
                       {
                           id = a.Id,
                           name = a.Name,
                           status = a.status
                       };
            }
            catch
            {
                return null;
            }
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
        public bool AddFoods(Object.Input.InputFood updateMenu)
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

        public string AddFood(Object.Input.InsertFood f)
        {
            try
            {
                string id = setId.GetFoodId();

                Food food = new Food
                {
                    Id = id,
                    Name = f.name,
                    Price = f.price,
                    Unit = f.unit,
                    MenuId = f.menuId,
                    CategoryId = f.categoryId,
                };
                db.Foods.Add(food);
                db.SaveChanges();

                return id;
            }
            catch
            {
                return "null";
            }
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

        public bool delFood(string foodId)
        {
            try
            {
                Food food = db.Foods.Find(foodId);
                db.Remove(food);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Object.Get.GetFood> foodListByReserveTableId(string reserveTableId)
        {
            try
            {
                var result = from a in db.ReserveFoods
                             where a.ReserveTable == reserveTableId
                             select new Object.Get.GetFood()
                             {
                                 foodId = a.Food.Id,
                                 name = a.Food.Name,
                                 price = a.Food.Price,
                                 unit = a.Food.Unit,
                                 menuName = a.Food.Menu.Name,
                                 categoryName = a.Food.Category.Name,
                                 pic = (List<string>)(from c in db.Images
                                                      where c.FoodId == a.Food.Id
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

        public IEnumerable<Object.Get.GetPromotion1> getPromotions(string restaurantId)
        {
            try
            {
                return from a in db.Promotions
                       where a.RestaurantId == restaurantId
                       select new Object.Get.GetPromotion1()
                       {
                           promotionId = a.Id,
                           name = a.Name,
                           value = a.Value,
                           info = a.Info,
                       };
            }
            catch
            {
                return null;
            }
        }

        public bool delPromotion(string promotionId) 
        {
            try
            {
                var promotion = db.Promotions.Find(promotionId);
                db.Remove(promotion);

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