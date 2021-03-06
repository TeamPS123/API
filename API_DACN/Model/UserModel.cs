using API_DACN.Database;
using API_DACN.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model
{
    public class UserModel
    {
        private readonly food_location_dbContext db;
        private NextId setId;

        public UserModel(food_location_dbContext db)
        {
            this.db = db;
            setId = new NextId(db);
        }

        public IEnumerable<Object.Get.GetReserveTable> getReserveTables (string userId, LngLat lngLat)
        {
            return from reserveTable in db.ReserveTables
                   where reserveTable.UserId == userId
                   select new Object.Get.GetReserveTable()
                   {
                       Id = reserveTable.Id,
                       quantity = reserveTable.QuantityPeople,
                       time = reserveTable.Time,
                       promotionId = reserveTable.PromotionId,
                       name = reserveTable.Name,
                       phone = reserveTable.PhoneNumber,
                       note = reserveTable.Note,
                       status = reserveTable.Status,
                       restaurant = (from b in db.Restaurants
                                     where b.Id == reserveTable.RestaurantId
                                     select new Object.Get.GetRestaurant()
                                     {
                                         userId = b.UserId,
                                         restaurantId = b.Id,
                                         name = b.Name,
                                         line = b.Line,
                                         city = b.City,
                                         district = b.District,
                                         longLat = b.LongLat,
                                         openTime = b.OpenTime,
                                         closeTime = b.CloseTime,
                                         distance = Distance.distance(b.LongLat, lngLat).ToString(),
                                         phoneRes = b.PhoneRestaurant,
                                         status = b.Status,
                                         statusCO = b.StatusCo,
                                         mainPic = db.Images.Where(t => t.RestaurantId == b.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                         pic = GetImage.getImageWithRes(b.Id, db),
                                         categoryResStr = Other.Convert.ConvertListToString(b.RestaurantDetails.Select(c => c.Category.Name).ToList()),
                                         promotionRes = (from c in b.Promotions
                                                         where c.Status == true
                                                         select new Object.Get.GetPromotion_Res()
                                                        {
                                                            id = c.Id,
                                                            name = c.Name,
                                                            info = c.Info,
                                                            value = c.Value
                                                        }).ToList(),
                                         categoryRes = (from d in b.RestaurantDetails
                                                       select new Object.Get.GetCategoryRes()
                                                       {
                                                           id = d.CategoryId,
                                                           name = d.Category.Name,
                                                           icon = d.Category.Icon
                                                       }).ToList()
                                     }).FirstOrDefault()
                   };
        }

        public Object.Get.GetReserveTable getReserveTable(string reserveTableId)
        {
            return (from reserveTable in db.ReserveTables
                   where reserveTable.Id == reserveTableId
                   select new Object.Get.GetReserveTable()
                   {
                       Id = reserveTable.Id,
                       quantity = reserveTable.QuantityPeople,
                       time = reserveTable.Time,
                       promotionId = reserveTable.PromotionId,
                       name = reserveTable.Name,
                       phone = reserveTable.PhoneNumber,
                       note = reserveTable.Note,
                       status = reserveTable.Status,
                       restaurant = (from b in db.Restaurants
                                     where b.Id == reserveTable.RestaurantId
                                     select new Object.Get.GetRestaurant()
                                     {
                                         userId = b.UserId,
                                         restaurantId = b.Id,
                                         name = b.Name,
                                         line = b.Line,
                                         city = b.City,
                                         district = b.District,
                                         longLat = b.LongLat,
                                         openTime = b.OpenTime,
                                         closeTime = b.CloseTime,
                                         distance = "Không xác đinh",
                                         phoneRes = b.PhoneRestaurant,
                                         status = b.Status,
                                         statusCO = b.StatusCo,
                                         mainPic = db.Images.Where(t => t.RestaurantId == b.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                         pic = GetImage.getImageWithRes(b.Id, db),
                                         categoryResStr = Other.Convert.ConvertListToString(b.RestaurantDetails.Select(c => c.Category.Name).ToList()),
                                         promotionRes = (from c in b.Promotions
                                                         where c.Status == true
                                                         select new Object.Get.GetPromotion_Res()
                                                        {
                                                            id = c.Id,
                                                            name = c.Name,
                                                            info = c.Info,
                                                            value = c.Value
                                                        }).ToList(),
                                         categoryRes = (from d in b.RestaurantDetails
                                                       select new Object.Get.GetCategoryRes()
                                                       {
                                                           id = d.CategoryId,
                                                           name = d.Category.Name,
                                                           icon = d.Category.Icon
                                                       }).ToList()
                                     }).FirstOrDefault()
                   }).FirstOrDefault();
        }

        //status =>0: chờ xác nhận || 1: xác nhận || 2: hủy || 3: quá hạn || 4: từ chối
        public string ReserveTable(Object.Input.InputReserveTable input)
        {
            try
            {
                string id = setId.GetReserveTableId();
                ReserveTable reserve = new ReserveTable()
                {
                    Id = id,
                    QuantityPeople = input.quantity,
                    Time = input.time,
                    Status = 0,
                    RestaurantId = input.restaurantId,
                    PromotionId = input.promotionId,
                    UserId = input.userId,
                    Name = input.name,
                    PhoneNumber = input.phone,
                    Note = input.note,
                    Day = Other.Convert.ConvertDateTimeToString_FromClient(input.time)
                };
                db.ReserveTables.Add(reserve);
                db.SaveChanges();
                return id;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public bool ConfirmTable(Object.Input.ConfirmTable input)
        {
            try
            {
                ReserveTable table = db.ReserveTables.Find(input.reserveTableId);

                table.Status = input.status;
                db.SaveChanges();
            }catch
            {
                return false;

            }
            return true;
        }

        public bool ReserveFood(Object.Input.InputReserveFood input)
        {
            try
            {
                foreach (var item in input.foods)
                {
                    ReserveFood reserve = new ReserveFood()
                    {
                        FoodId = item.foodId,
                        ReserveTable = input.reserveTableId,
                        Price = item.price,
                        Quantity = item.quantity,
                    };
                    db.ReserveFoods.Add(reserve);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Object.Get.GetUser getUser(string userId)
        {
            try
            {
                var data = db.Users.Find(userId);

                return new Object.Get.GetUser()
                {
                    fullName = data.FullName,
                    phone = data.PhoneNumber,
                    isBusiness = data.IsBusiness,
                    gender = data.Gender,
                    pic = db.Images.Where(t => t.UserId == userId && t.RestaurantId == "0")
                            .Select(c => c.Link).FirstOrDefault()
                };
            }
            catch
            {
                return null;
            }
        }

        public bool updateUser(Object.Update.UpdateUser user)
        {
            try
            {
                var data = db.Users.Find(user.userId);
                data.FullName = user.fullName;
                data.PassswordHash = MD5.CreateMD5(user.pass);
                data.IsBusiness = user.isBusiness;
                data.Gender = user.gender;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Object.Get.GetNotification> getNotifications(string userId)
        {
            try
            {
                return (from a in db.ReserveTables
                       where a.Status != 0
                       orderby a.Id descending
                       select new Object.Get.GetNotification()
                       {
                           resName = db.Restaurants.Where(t => t.Id == a.RestaurantId).Select(c => c.Name).FirstOrDefault(),
                           img = db.Images.Where(t => t.RestaurantId == a.RestaurantId && t.FoodId != "0").Select(c => c.Link).FirstOrDefault(),
                           time = a.Time,
                           userId = a.UserId,
                           reserveTableId = a.Id,
                           status = a.Status,
                           resId = a.RestaurantId,
                       }).Take(5);

            }
            catch
            {
                return null;
            }
        }

        public string ratingRes(Object.Input.InputComment comment)
        {
            try
            {
                var rating = new Database.Rate();
                rating.Content = comment.content;
                rating.Value = comment.value;
                rating.UserId = comment.userId;
                rating.RestaurantId = comment.RestaurantId;
                rating.Date = comment.date;
                db.Rates.Add(rating);
                db.SaveChanges();

                string id = db.Rates.OrderByDescending(c => c.Id).Select(t => t.Id).FirstOrDefault()+"";

                return id;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public string review(Object.Input.InputComment review)
        {

            try
            {
                var reviewRes = new Database.Review();
                reviewRes.Content = review.content;
                reviewRes.Value = review.value;
                reviewRes.UserId = review.userId;
                reviewRes.RestaurantId = review.RestaurantId;
                reviewRes.Date = review.date;
                reviewRes.CountLike = 0;
                db.Reviews.Add(reviewRes);
                db.SaveChanges();

                string id = db.Reviews.OrderByDescending(c => c.Id).Select(t => t.Id).FirstOrDefault() + "";

                return id;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool likeAndDisReview(string userId, int reviewId)
        {
            try
            {
                var result = db.UserLikes.Where(t => t.UserId == userId && t.ReviewId == reviewId).FirstOrDefault();
                if(result != null)
                {
                    result.Status = !result.Status;
                    db.SaveChanges();

                    return true;
                }
                var likeAndDis = new Database.UserLike();
                likeAndDis.UserId = userId;
                likeAndDis.ReviewId = reviewId;
                likeAndDis.Status = true;

                db.UserLikes.Add(likeAndDis);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public string addComment(string userId, int reviewId, string content, string date)
        {
            try
            {
                var comment = new Database.UserComment();
                comment.UserId = userId;
                comment.ReviewId = reviewId;
                comment.Content = content;
                comment.Date = date;
                db.UserComments.Add(comment);
                db.SaveChanges();

                string id = db.UserComments.OrderByDescending(c => c.Id).Select(t => t.Id).FirstOrDefault() + "";

                return id + "";
            }
            catch(Exception e)
            {
                return "null";
            }
        }
    
        public IEnumerable<Object.Get.GetComment> comments(int reviewId, int skip, int take)
        {
            try
            {
                return (from a in db.UserComments
                        where a.ReviewId == reviewId
                        orderby a.Id descending
                        select new Object.Get.GetComment()
                        {
                            name = a.User.FullName,
                            imgUser = db.Images.Where(t => t.UserId == a.UserId && t.RestaurantId == "0").Select(c => c.Link).FirstOrDefault(),
                            content = a.Content,
                            date = a.Date,
                        }).Skip(skip).Take(take);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object.Get.GetLike> getLike(int reviewId)
        {
            try
            {
                Object.Get.GetLike likeReview = new Object.Get.GetLike();

                return (from a in db.UserLikes
                            where a.ReviewId == reviewId && a.Status == true
                            orderby a.Id descending
                            select new Object.Get.GetLike()
                            {
                                userId = a.UserId,
                                name = a.User.FullName
                            }).Take(5).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
