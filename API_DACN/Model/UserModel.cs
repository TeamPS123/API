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
                       note = reserveTable.note,
                       restaurant = (from b in db.Restaurants
                                     where b.Id == reserveTable.RestaurantId
                                     select new Object.Get.GetRestaurant()
                                     {
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
                                         mainPic = db.Images.Where(t => t.RestaurantId == b.Id && t.FoodId == "0").Select(c => c.Link).FirstOrDefault(),
                                         pic = GetImage.getImageWithRes(b.Id, db),
                                         categoryResStr = Other.Convert.ConvertListToString(b.RestaurantDetails.Select(c => c.Category.Name).ToList()),
                                         promotionRes = from c in b.Promotions
                                                        select new Object.Get.GetPromotion_Res()
                                                        {
                                                            id = c.Id,
                                                            name = c.Name,
                                                            info = c.Info,
                                                            value = c.Value
                                                        },
                                         categoryRes = from d in b.RestaurantDetails
                                                       select new Object.Get.GetCategoryRes()
                                                       {
                                                           id = d.CategoryId,
                                                           name = d.Category.Name,
                                                           icon = d.Category.icon
                                                       }
                                     }).FirstOrDefault()
                   };
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
                    note = input.note
                };
                db.ReserveTables.Add(reserve);
                db.SaveChanges();
                return id;
            }
            catch
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
    }
}
