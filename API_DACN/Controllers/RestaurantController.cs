using API_DACN.Database;
using API_DACN.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private Other.Token Token;
        private RestaurantModel res_model;

        public RestaurantController(IConfiguration config, food_location_dbContext db)
        {
            Token = new Other.Token(config, db);
            res_model = new RestaurantModel(db);
        }

        [Route("getInfoRestaurant")]
        [HttpGet]
        public IActionResult getInfoRestaurant(string userId, string restaurantId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Get.MessageInfoRes(2, "Kiểm tra lại token tý nào", null));
            }
            var result = res_model.getInfoRes(restaurantId);
            if (result == null)
            {
                return Ok(new Object.Get.MessageInfoRes(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.MessageInfoRes(1, "Lấy dữ liệu thành công", result));
        }

        [Route("getRestaurantId")]
        [HttpGet]
        public IActionResult getRestaurantId(string userId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = res_model.retaurantId(userId);
            if (result == "null")
            {
                return Ok(new Object.Message(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Message(1, "Lấy dữ liệu thành công", result));
        }

        [Route("getAllReserveTableByRestaurantId")]
        [HttpGet]
        public IActionResult getAllReserverTableByRestaurantId(string userId, string restaurantId, int code)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = res_model.getAllReserverTableByRestaurantId(restaurantId, code);

            if (result == null)
            {
                return Ok(new Object.Get.Message_ReserveTable1(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ReserveTable1(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getQuantityReserveTable")]
        public IActionResult getQuantityReserveTable(string userId, string restaurantId, int code)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }
            var result = res_model.getQuantityReserveTable(restaurantId, code);

            if (result == "null")
            {
                return Ok(new Object.Message(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Message(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getAllMenuByResId")]
        public IActionResult getAllMenuByResId(string userId, string restaurantId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.menuListWithRes(restaurantId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_MenuList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_MenuList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("updateReserveTable")]
        public IActionResult updateReserveTable(string userId, string reserveTableId, int code)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.updateReserveTable(reserveTableId, code);

            if (!result)
            {
                return Ok(new Object.Get.Message_MenuList(0, "Cập nhật phiếu đặt bàn thất bại", null));
            }
            return Ok(new Object.Get.Message_MenuList(1, "Cập nhật phiếu đặt bàn thành công", null));
        }

        [HttpGet]
        [Route("getAllCategory")]
        public IActionResult getAllCategory(string userId, string restaurantId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.getAllCatelogy(restaurantId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_Category(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_Category(1, "Lấy dữ liệu thành công", result));
        }

        [Route("addRestaurant")]
        [HttpPost]
        public IActionResult AddRestaurant(Object.Input.InputRestaurant restaurant)
        { 
            if (Token.GetPhoneWithToken(Request.Headers) != restaurant.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var add = res_model.AddRestaurant(restaurant);
            if (add.Equals("null"))
            {
                return Ok(new Object.Message(0, "Thêm nhà hàng thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm nhà hàng thành công", add));
        }

       [HttpPost]
       [Route("updateRestaurant")]
       public IActionResult updateRestaurant(Object.Update.UpdateRestaurant restaurant)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != restaurant.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.updateRestaurant(restaurant);
            if (!result)
            {
                return Ok(new Object.Message(0, "Cập nhật nhà hàng thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật nhà hàng thành công", null));
        }

        [Route("addMenu")]
        [HttpPost]
        public IActionResult AddMenu(Object.Input.InputMenu menu)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != menu.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var add = res_model.CreateMenu(menu);
            if (add.Equals("null"))
            {
                return Ok(new Object.Message(0, "Thêm thực đơn thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm thực đơn thành công", add));
        }

        [HttpPost]
        [Route("updateMenu")]
        public IActionResult updateMenu(Object.Update.UpdateMenu menu)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != menu.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.updateMenu(menu);
            if (!result)
            {
                return Ok(new Object.Message(0, "Cập nhật thực đơn thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật thực đơn thành công", null));
        }

        [Route("addCategory")]
        [HttpPost]
        public IActionResult AddCategory(Object.Input.InputCategory category)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != category.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var add = res_model.CreateCategory(category);
            if (add.Equals("null"))
            {
                return Ok(new Object.Message(0, "Thêm loại thức ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm loại thức ăn thành công", add));
        }

        //------------------------------food-----------------------------------
        [Route("addFoods")]
        [HttpPost]
        public IActionResult AddFoods(Object.Input.InputFood menu)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != menu.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.AddFoods(menu);
            if (result == false)
            {
                return Ok(new Object.Message(0, "Thêm thức ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm thức ăn thành công", null));
        }

        [Route("addFood")]
        [HttpPost]
        public IActionResult AddFood(Object.Input.InsertFood food)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != food.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.AddFood(food);
            if (result == "null")
            {
                return Ok(new Object.Message(0, "Thêm thức ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm thức ăn thành công", result));
        }

        [HttpPost]
        [Route("updateFood")]
        public IActionResult updateFood(Object.Update.UpdateFood food)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != food.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.updateFood(food);
            if (result == false)
            {
                return Ok(new Object.Message(0, "Cập nhật thức ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật thức ăn thành công", null));
        }

        [HttpGet]
        [Route("delFood")]
        public IActionResult delFood(string userId, string foodId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.delFood(foodId);
            if (result == false)
            {
                return Ok(new Object.Message(0, "Xóa thức ăn thất bại", null));
            }
            return Ok(new Object.Message(1, "Xóa thức ăn thành công", null));
        }

        //------------------------------promotion-----------------------------------
        [Route("addPromotion")]
        [HttpPost]
        public IActionResult AddPromotion(Object.Input.InputPromotion promotion)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != promotion.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.CreatePromotion(promotion);
            if (result.Equals("null"))
            {
                return Ok(new Object.Message(0, "Thêm khuyến mãi thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm khuyến mãi thành công", result));
        }

        [HttpPost]
        [Route("updatePromotion")]
        public IActionResult updatePromotion(Object.Update.UpdatePromotion promotion)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != promotion.userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.UpdatePromotion(promotion);
            if (result == false)
            {
                return Ok(new Object.Message(0, "Cập nhật khuyến mãi thất bại", null));
            }
            return Ok(new Object.Message(1, "Cập nhật khuyến mãi thành công", null));
        }

        [HttpGet]
        [Route("getPromotionList")]
        public IActionResult getPromotionList(string userId, string restaurantId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Get.Message_Promotion(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.getPromotions(restaurantId);
            if (result != null)
            {
                return Ok(new Object.Get.Message_Promotion(0, "Lấy dữ liệu thành công", null));
            }
            return Ok(new Object.Get.Message_Promotion(1, "Lấy dữ liệu thất bại", result));
        }

        [HttpGet]
        [Route("delPromotion")]
        public IActionResult delPromotion(string userId, string promotionId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            var result = res_model.delPromotion(promotionId);
            if (result.Equals("null"))
            {
                return Ok(new Object.Message(0, "Thêm khuyến mãi thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm khuyến mãi thành công", null));
        }
    }
}