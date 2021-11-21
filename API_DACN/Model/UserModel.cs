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

        //status => chờ xác nhận || 1: xác nhận || 2: quá hạn
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
