using API_DACN.Database;
using API_DACN.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserModel userModel;
        private Other.Token token;

        public UserController(IConfiguration config, food_location_dbContext db)
        {
            userModel = new UserModel(db);
            token = new Other.Token(config, db);
        }

        [Route("getAllReserverTable")]
        [HttpPost]
        public IActionResult getAllReserverTable(Object.Input.InputGetReserveTable input)
        {
            if (token.GetPhoneWithToken(Request.Headers) != input.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.getReserveTables(input.userId, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ReserveTable(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ReserveTable(1, "Lấy dữ liệu thành công", result));
        }

        [Route("reserveTable")]
        [HttpPost]
        public IActionResult ReserveTable(Object.Input.InputReserveTable input)
        {
            if (token.GetPhoneWithToken(Request.Headers) != input.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.ReserveTable(input);

            if (result == null)
            {
                return Ok(new Object.Message(0, "Đặt bàn thất bại", null));
            }
            return Ok(new Object.Message(1, "Đặt bàn thành công", result));
        }

        [Route("confirmTable")]
        [HttpPost]
        public IActionResult ConfirmTable(Object.Input.ConfirmTable input)
        {
            if (token.GetPhoneWithToken(Request.Headers) != input.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.ConfirmTable(input);

            if (result == false)
            {
                return Ok(new Object.Message(0, "Cập nhật tình trạng thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật tình trạng thành công", null));
        }

        [Route("reserveFood")]
        [HttpPost]
        public IActionResult ReserveFood(Object.Input.InputReserveFood input)
        {
            if (token.GetPhoneWithToken(Request.Headers) != input.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.ReserveFood(input);

            if (result == false)
            {
                return Ok(new Object.Message(0, "Đặt đồ ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Đặt đồ ăn thành công", null));
        }

        [Route("getInfo")]
        [HttpGet]
        public IActionResult getInfo(string userId)
        {
            if (token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Get.MessageUser(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.getUser(userId);

            if (result == null)
            {
                return Ok(new Object.Get.MessageUser(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.MessageUser(1, "Lấy dữ liệu thành công", result));
        }

        [HttpPost]
        [Route("updateInfo")]
        public IActionResult updateInfo(Object.Update.UpdateUser user)
        {
            if (token.GetPhoneWithToken(Request.Headers) != user.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = userModel.updateUser(user);

            if (result == false)
            {
                return Ok(new Object.Message(0, "Cập nhật thông tin thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật thông tin thành công", null));
        }
    }
}
