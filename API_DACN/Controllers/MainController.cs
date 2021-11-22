using API_DACN.Database;
using API_DACN.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private RestaurantModel model;
        private SearchModel modelSearch;

        public MainController(food_location_dbContext db)
        {
            model = new RestaurantModel(db);
        }

        [HttpGet]
        [Route("getRestaurant")]
        public IActionResult getRestaurant(string restaurantId)
        {
            var result = model.getRestaurant(restaurantId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_Res(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_Res(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getResWithPromotion")]
        public IActionResult getResWithPromotion(string promotionId)
        {
            var result = model.getResWithPromotion(promotionId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_Res(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_Res(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getAllRestaurant")]
        public IActionResult getAllRestaurant()
        {
            var result = model.restaurantList();
            var result1 = modelSearch.categoryResList(result);
            var result2 = modelSearch.districtList(result);

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpGet]
        [Route("getAllCategoryRes")]
        public IActionResult getAllCategoryRes()
        {
            var result = model.categoryList();

            if (result == null)
            {
                return Ok(new Object.Get.Message_CategoryResList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_CategoryResList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getMenu")]
        public IActionResult getMenu(string menuId)
        {
            var result = model.getMenu(menuId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_Menu(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_Menu(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getAllMenu")]
        public IActionResult getAllMenu()
        {
            var result = model.menuList();

            if (result == null)
            {
                return Ok(new Object.Get.Message_MenuList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_MenuList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getFood")]
        public IActionResult getFood(string foodId)
        {
            var result = model.getFood(foodId);

            if (result == null)
            {
                return Ok(new Object.Get.Message_Food(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_Food(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getAllFood")]
        public IActionResult getAllFood()
        {
            var result = model.foodList();

            if (result == null)
            {
                return Ok(new Object.Get.Message_FoodList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_FoodList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpGet]
        [Route("getAllPromotion")]
        public IActionResult getAllPromotion(Object.Input.InputLocation location) { 
            var result = model.promotionList(new Other.LngLat(location.lon, location.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ProList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ProList(1, "Lấy dữ liệu thành công", result));
        }
    }
}
