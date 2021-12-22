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
                           icon = a.Icon,
                       };
            }
            catch
            {
                return null;
            }
        }


        //------------------------------restaurant-----------------------------------
        public List<Object.Get.GetStatis> getStatis(string restaurantId, string month1, string year1, string month2, string year2)
        {
            try
            {
                IEnumerable<Database.ReserveTable> reserveTables = from a in db.ReserveTables
                                                                   where a.RestaurantId == restaurantId
                                                                   select a;

                List<Object.Get.GetStatis> getStatistics = new List<Object.Get.GetStatis>();

                if(year1 == year2)
                {
                    for(int i = int.Parse(month1); i <= int.Parse(month2); i++)
                    {
                        getStatistics.Add(getStatistic(reserveTables, i+"", year1));
                    }
                }
                else
                {
                    for(int i = int.Parse(year1); i <= int.Parse(year2); i++)
                    {
                        if(i == int.Parse(year1))
                        {
                            for (int j = int.Parse(month1); j <= 12; j++)
                            {
                                getStatistics.Add(getStatistic(reserveTables, j+"", i+""));
                            }
                        }
                        else if(i == int.Parse(year2))
                        {

                            for (int j = 1; j <= int.Parse(month2); j++)
                            {
                                getStatistics.Add(getStatistic(reserveTables, j + "", i + ""));
                            }
                        }
                        else
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                getStatistics.Add(getStatistic(reserveTables, j + "", i + ""));
                            }
                        }
                    }
                }

                return getStatistics;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private Object.Get.GetStatis getStatistic(IEnumerable<Database.ReserveTable> reserveTables, string month, string year)
        {
            List<Database.ReserveTable> reserveTables1 = new List<ReserveTable>();
            foreach (var item in reserveTables)
            {
                if (Other.Date.checkDate(item.Day, 0 + "", month, year) == true)
                {
                    reserveTables1.Add(item);
                }
            }

            Object.Get.GetStatis getStatis = new Object.Get.GetStatis();
            if (int.Parse(month) != 0)
            {
                getStatis.time = month + "/" + year;
            }
            else
            {
                getStatis.time = year;
            }
            getStatis.amountComplete = reserveTables1.Where(t => t.Status == 4).Count() + "";
            getStatis.amountExpired = reserveTables1.Where(t => t.Status == 3).Count() + "";

            return getStatis;
        }

        public List<Object.Get.GetStatis> getStatisWithYear(string restaurantId, string year1, string year2)
        {
            try
            {
                IEnumerable<Database.ReserveTable> reserveTables = from a in db.ReserveTables
                                                                   where a.RestaurantId == restaurantId
                                                                   select a;

                List<Object.Get.GetStatis> getStatistics = new List<Object.Get.GetStatis>();

                for (int i = int.Parse(year1); i <= int.Parse(year2); i++)
                {
                    getStatistics.Add(getStatistic(reserveTables, "0", i+""));
                }

                return getStatistics;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetRating> getAllRate(string restaurantId, int value, int skip, int take)
        {
            try
            {
                var result = (from a in db.Rates
                              where a.RestaurantId == restaurantId
                              orderby a.Date descending
                              select new Object.Get.GetRating()
                              {
                                  Id = a.Id,
                                  content = a.Content,
                                  value = a.Value,
                                  UserId = a.UserId,
                                  RestaurantId = a.RestaurantId,
                                  date = a.Date,
                                  UserName = a.User.FullName,
                                  imageUser = db.Images.Where(t => t.UserId == a.UserId && t.RestaurantId == "0").Select(c => c.Link).FirstOrDefault(),
                              }).Skip(skip).Take(take);

                if(value != -1)
                {
                    result = result.Where(t => t.value == value);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public string rateTotal(string restaurantId)
        {
            try
            {
                var result = db.Rates.Where(t => t.RestaurantId == restaurantId);

                float count = result.Count();
                if(count == 0)
                {
                    return "0";
                }

                float sum = result.Sum(t => t.Value);

                return (sum / count).ToString("0.0");
            }
            catch
            {
                return "0";
            }
        }

        public Object.Get.GetCountRating getCountRating(string restaurantId)
        {
            try
            {
                var rates = db.Rates.Where(t => t.RestaurantId == restaurantId);

                var result = new Object.Get.GetCountRating();
                result.count = rates.Count()+"";
                result.count1 = rates.Where(t => t.Value == 1).Count() + "";
                result.count2 = rates.Where(t => t.Value == 2).Count() + "";
                result.count3 = rates.Where(t => t.Value == 3).Count() + "";
                result.count4 = rates.Where(t => t.Value == 4).Count() + "";
                result.count5 = rates.Where(t => t.Value == 5).Count() + "";

                return result;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetReview> getAllReview(int skip, int take)
        {
            try
            {
                var result = (from a in db.Reviews
                              orderby a.Id descending
                              select new Object.Get.GetReview()
                              {
                                  Id = a.Id,
                                  content = a.Content,
                                  value = a.Value,
                                  UserId = a.UserId,
                                  RestaurantId = a.RestaurantId,
                                  imageRes = db.Images.Where(t => t.FoodId == "0" && t.RestaurantId == a.RestaurantId).Select(c => c.Link).FirstOrDefault(),
                                  date = a.Date,
                                  UserName = db.Users.Where(t => t.Id == a.UserId).Select(c => c.FullName).FirstOrDefault(),
                                  countLike = a.CountLike,
                                  imageUser = db.Images.Where(t => t.UserId == a.UserId && t.RestaurantId == "0").Select(c => c.Link).FirstOrDefault(),
                                  imgList = db.Images.Where(t => t.ReviewId == a.Id).Select(c => c.Link).ToList(),
                                  userList = (from b in db.UserLikes
                                              where b.ReviewId == a.Id && b.Status == true
                                              select new Object.Get.GetLike()
                                              {
                                                  userId = b.UserId,
                                                  name = b.User.FullName
                                              }).ToList(),
                                  comments = (from c in db.UserComments
                                              where c.ReviewId == a.Id
                                              orderby c.Id descending
                                              select new Object.Get.GetComment()
                                              {
                                                  name = c.User.FullName,
                                                  imgUser = db.Images.Where(t => t.UserId == c.UserId && t.RestaurantId == "0").Select(c => c.Link).FirstOrDefault(),
                                                  content = c.Content,
                                                  date = c.Date,
                                              }).ToList()
                              }).Skip(skip).Take(take);

                //if (value != -1)
                //{
                //    result = result.Where(t => t.value == value);
                //}

                return result;
            }
            catch
            {
                return null;
            }
        }

        public string reviewTotal()
        {
            try
            {
                var result = db.Reviews;

                float count = result.Count();
                float sum = result.Sum(t => t.Value);

                return (sum / count).ToString("0.0");
            }
            catch
            {
                return "0";
            }
        }

        public Object.Get.GetCountRating getCountReview()
        {
            try
            {
                var rates = db.Reviews;

                var result = new Object.Get.GetCountRating();
                result.count = rates.Count() + "";
                result.count1 = rates.Where(t => t.Value == 1).Count() + "";
                result.count2 = rates.Where(t => t.Value == 2).Count() + "";
                result.count3 = rates.Where(t => t.Value == 3).Count() + "";
                result.count4 = rates.Where(t => t.Value == 4).Count() + "";
                result.count5 = rates.Where(t => t.Value == 5).Count() + "";

                return result;
            }
            catch
            {
                return null;
            }
        }

        public bool checkRes(string userId)
        {
            try
            {
                if(db.Restaurants.Where(t => t.UserId == userId).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false; 
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

        public Object.Get.getResDetail getResDetail(List<string> days, string toDay, string restaurantId)
        {
            try
            {
                Object.Get.getResDetail resDetail = new Object.Get.getResDetail();

                resDetail.amountDay = (from a in db.ReserveTables
                                       where a.RestaurantId == restaurantId && a.Time.Contains(toDay)
                                       select a).Count();

                resDetail.amountWeek = (from a in db.ReserveTables
                                        where a.RestaurantId == restaurantId && days.Contains(a.Day) == true
                                        select a).Count();

                resDetail.status = db.Restaurants.Find(restaurantId).Status;

                return resDetail;
            }
            catch
            {
                return null;
            }
        }

        public bool changeStatus(string restaurantId, bool status)
        {
            try
            {
                var res = db.Restaurants.Find(restaurantId);
                res.Status = status;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
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
                data.StatusCo = restaurant.statusCO;
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
                             where a.Id == restaurantId
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
                                 status = a.Status,
                                 statusCO = a.StatusCo,
                                 mainPic = db.Images.Where(t => t.RestaurantId == restaurantId && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                 pic = GetImage.getImageWithRes(restaurantId, db),
                                 categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name).ToList()), 
                                 promotionRes = (from c in db.Promotions
                                                where c.RestaurantId == a.Id
                                                select new Object.Get.GetPromotion_Res()
                                                {
                                                    id = c.Id,
                                                    name = c.Name,
                                                    info = c.Info,
                                                    value = c.Value
                                                }).ToList(),
                                 categoryRes = (from b in db.RestaurantDetails
                                               where b.RestaurantId == a.Id
                                               select new Object.Get.GetCategoryRes()
                                               {
                                                   id = b.CategoryId,
                                                   name = b.Category.Name,
                                                   icon = b.Category.Icon
                                               }).ToList()
                             };

                return result.FirstOrDefault();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public Object.Get.GetRestaurant getResWithPromotion(string promotionId)
        {
            try
            {
                var result = from a in db.Promotions
                             where a.Id == promotionId
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
                                 status = a.Restaurant.Status,
                                 statusCO = a.Restaurant.StatusCo,
                                 mainPic = db.Images.Where(t => t.RestaurantId == a.RestaurantId && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                 pic = GetImage.getImageWithRes(a.RestaurantId, db),
                                 categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.RestaurantId).Select(c => c.Category.Name).ToList()),
                                 promotionRes = (from c in db.Promotions
                                                where c.RestaurantId == a.RestaurantId
                                                select new Object.Get.GetPromotion_Res()
                                                {
                                                    id = c.Id,
                                                    name = c.Name,
                                                    info = c.Info,
                                                    value = c.Value
                                                }).ToList(),
                                 categoryRes = (from b in db.RestaurantDetails
                                               where b.RestaurantId == a.RestaurantId
                                               select new Object.Get.GetCategoryRes()
                                               {
                                                   id = b.CategoryId,
                                                   name = b.Category.Name,
                                                   icon = b.Category.Icon
                                               }).ToList()
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
                                 status = a.Status,
                                 statusCO = a.StatusCo,
                                 mainPic = db.Images.Where(t => t.RestaurantId == a.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                 pic = GetImage.getImageWithRes(a.Id, db),
                                 categoryResStr = Other.Convert.ConvertListToString(db.RestaurantDetails.Where(t => t.RestaurantId == a.Id).Select(c => c.Category.Name).ToList()),
                                 promotionRes = (from c in db.Promotions
                                                where c.RestaurantId == a.Id
                                                select new Object.Get.GetPromotion_Res()
                                                {
                                                    id = c.Id,
                                                    name = c.Name,
                                                    info = c.Info,
                                                    value = c.Value
                                                }).ToList(),
                                 categoryRes = (from b in db.RestaurantDetails
                                               where b.RestaurantId == a.Id
                                               select new Object.Get.GetCategoryRes()
                                               {
                                                   id = b.CategoryId,
                                                   name = b.Category.Name,
                                                   icon = b.Category.Icon
                                               }).ToList()
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
                       note = reserveTable.Note,
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
                                             categoryId = b.CategoryId,
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
                                             categoryId = b.CategoryId,
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
                                             categoryId = b.CategoryId,
                                             status = b.Status,
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
                c.Status = true;
                c.KeyWord = Regex1.RemoveUnicode(category.name);
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
                           status = a.Status
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
                                 icon = a.Icon,
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
                    Status = true,
                    KeyWord = Regex1.RemoveUnicode(f.name),
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
                var data = db.Foods.Find(food.foodId);
                data.Name = food.name;
                data.Price = food.price;
                data.Unit = food.unit;
                data.CategoryId = food.categoryId;
                data.Status = food.status;
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

        public IEnumerable<Object.Get.FoodOfMenu> getFoodsByResId(string reserveTableId)
        {
            try
            {
                var result = from a in db.ReserveFoods
                             where a.ReserveTable == reserveTableId
                             select new Object.Get.FoodOfMenu()
                             {
                                 menuId = a.Food.MenuId,
                                 foodId = a.Food.Id,
                                 name = a.Food.Name,
                                 price = a.Food.Price,
                                 unit = a.Food.Unit,
                                 categoryId = a.Food.CategoryId,
                                 categoryName = a.Food.Category.Name,
                                 status = a.Food.Status,
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

        public IEnumerable<Object.Get.GetFoodOfReserveTable> foodListByReserveTableId(string reserveTableId)
        {
            try
            {
                var result = from a in db.ReserveFoods
                             where a.ReserveTable == reserveTableId
                             select new Object.Get.GetFoodOfReserveTable()
                             {
                                 foodId = a.Food.Id,
                                 name = a.Food.Name,
                                 price = a.Food.Price,
                                 unit = a.Food.Unit,
                                 menuName = a.Food.Menu.Name,
                                 categoryName = a.Food.Category.Name,
                                 amount = a.Quantity,
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
                    Status = true,
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
                data.Status = promotion.status;
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
                           status = a.Status,
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
                           status = a.Status,
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