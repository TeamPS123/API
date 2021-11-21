using API_DACN.Database;
using API_DACN.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model
{
    public class LoginModel
    {
        private readonly food_location_dbContext db;
        private NextId setId;

        public LoginModel(food_location_dbContext db)
        {
            this.db = db;
            setId = new NextId(db);
        }

        public bool SignUp(Object.UserSignUp user)
        {
            try
            {
                User u = new User();
                u.Id = setId.GetUserId();
                u.FullName = user.fullName;
                u.PhoneNumber = user.phone;
                u.PassswordHash = MD5.CreateMD5(user.pass);
                u.IsBusiness = user.business;
                u.Gender = user.gender;
                db.Users.Add(u);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CheckPhone(string phone)
        {
            var result = db.Users.Where(t => t.PhoneNumber == phone);

            if (result.Count() == 0)
                return false;
            return true;
        }

        public string Login(Object.UserLogin user)
        {
            var result = db.Users.Where(t => t.PhoneNumber == user.phone & t.PassswordHash == MD5.CreateMD5(user.pass)).Select(c => c.Id);

            if (result.Count() == 0)
                return "";
            return result.FirstOrDefault();
        }
    }
}
