using API_DACN.Database;
using API_DACN.Model;
using API_DACN.Object;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginModel userModel;
        private RestaurantModel resModel;
        private Other.Token token;

        public LoginController(IConfiguration config, food_location_dbContext db)
        {
            userModel = new LoginModel(db);
            resModel = new RestaurantModel(db);
            token = new Other.Token(config, db);
        }

        [Route("signup")]
        [HttpPost]
        public IActionResult SignUp(Object.UserSignUp user)
        {
            if (userModel.CheckPhone(user.phone) == true)
            {
                return Ok(new Message(2, "Số điện thoại đã có tài khoản", null));
            }

            var result = userModel.SignUp(user);

            if (result == null)
            {
                return Ok(new Message(0, "Đăng ký thất bại", null));
            }
            return Ok(new Message(1, token.GetToken(result), result));
        }

        [Route("checkPhone")]
        [HttpGet]
        public IActionResult checkPhone(string phone)
        {
            var check = userModel.CheckPhone(phone);
            if (check)
            {
                return Ok(new Message(0, "Số điện thoại đã có tài khoản", null));
            }
            return Ok(new Message(1, "Số điện thoại chưa có tài khoản", null));
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult Login(Object.UserLogin user)
        {
            var data = userModel.Login(user);
            if (data == "")
            {
                return Ok(new Message(0, "Tài khoản hoặc mật khẩu đã sai", null));
            }
            return Ok(new Message(1, token.GetToken(data), data));
        }

        //kiểm tra user đăng nhập vào ứng quản lí có nhà hàng chưa
        [Route("checkRes")]
        [HttpGet]
        public IActionResult checkRes(string userId)
        {
            var check = resModel.checkRes(userId);
            if (!check)
            {
                return Ok(new Object.Message(0, "Vui lòng đăng ký tài khoản dành cho quản lí", null));
            }
            return Ok(new Object.Message(1, "Tài khoản đã có nhà hàng", null));
        }
    }
}
