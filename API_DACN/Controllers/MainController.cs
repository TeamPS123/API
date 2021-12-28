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
        private UserModel userModel;
        food_location_dbContext db;

        public MainController(food_location_dbContext db)
        {
            model = new RestaurantModel(db);
            userModel = new UserModel(db);
            this.db = db;
        }

        [HttpGet]
        [Route("getCategoryRes")]
        public IActionResult getCategoryRes()
        {
            var result = model.getCategoryRes();

            if (result == null)
            {
                return Ok(new Object.Get.Message_CategoryResList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_CategoryResList(1, "Lấy dữ liệu thành công", result));
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
        [Route("getAllMenuWithRes")]
        public IActionResult getAllMenuWithRes(string restaurantId)
        {
            var result = model.menuListWithRes(restaurantId);

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

        //[HttpGet]
        //[Route("getAllFood")]
        //public IActionResult getAllFood()
        //{
        //    var result = model.foodList();

        //    if (result == null)
        //    {
        //        return Ok(new Object.Get.Message_FoodList(0, "Lấy dữ liệu thất bại", null, null));
        //    }
        //    return Ok(new Object.Get.Message_FoodList(1, "Lấy dữ liệu thành công", null, result));
        //}

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

        [HttpGet]
        [Route("getAllRateRes")]
        public IActionResult getAllRateRes(string restaurantId, int value, int skip, int take)
        {
            var rates = model.getAllRate(restaurantId, value, skip, take);

            if (rates == null)
            {
                return Ok(new Object.Get.Message_Rate(0, "Lấy dữ liệu thất bại", "0", null, null));
            }

            var rateTotal = model.rateTotal(restaurantId);
            var count = model.getCountRating(restaurantId);
            return Ok(new Object.Get.Message_Rate(1, "Lấy dữ liệu thành công", rateTotal, count, rates));
        }

        [HttpGet]
        [Route("getAllReviewRes")]
        public IActionResult getAllReviewRes(int skip, int take)
        {
            var reviews = model.getAllReview(skip, take);

            if (reviews == null)
            {
                return Ok(new Object.Get.Message_Review(0, "Lấy dữ liệu thất bại", "0", null, null));
            }

            var rateTotal = model.reviewTotal();
            var count =  model.getCountReview();
            return Ok(new Object.Get.Message_Review(1, "Lấy dữ liệu thành công", "", null, reviews));
        }

        [HttpGet]
        [Route("getReview")]
        public IActionResult getReviewRes(int reviewId)
        {
            var reviews = model.getReview(reviewId);

            if (reviews == null)
            {
                return Ok(new Object.Get.Message_Review1(0, "Lấy dữ liệu thất bại", "0", null));
            }

            var rateTotal = model.reviewTotal();
            var count = model.getCountReview();
            return Ok(new Object.Get.Message_Review1(1, "Lấy dữ liệu thành công", "", reviews));
        }

        [HttpGet]
        [Route("getLikeAndComment")]
        public IActionResult getLikeAndComment(int reviewId, int skip, int take)
        {
            var comments = userModel.comments(reviewId, skip, take);
            var like = userModel.getLike(reviewId);

            if (comments == null && like == null)
            {
                return Ok(new Object.Get.GetLikeAndComment(0, "Lấy dữ liệu thất bại", null, null));
            }
            return Ok(new Object.Get.GetLikeAndComment(1, "Lấy dữ liệu thành công", like, comments));
        }

        [HttpGet]
        [Route("getStatisticResWithUser")]
        public IActionResult getStatisticResWithUser(string restaurantId)
        {
            string now = Other.Convert.ConvertDateTimeToString(DateTime.Now);
            var result = model.getStatisticResWithUser(restaurantId, now);
            if (result == "null")
            {
                return Ok(new Object.Message(0, "Lấy dữ liệu thất bại", null));
            }

            return Ok(new Object.Message(1, "Lấy dữ liệu thành công", result));
        }


        //[HttpGet]
        //[Route("tampthuinha")]
        //public IActionResult ttttt()
        //{
        //    Other.NextId t = new Other.NextId(db);
        //    t.temp("com", 20);
        //    return Ok(0);
        //}
    }
}
