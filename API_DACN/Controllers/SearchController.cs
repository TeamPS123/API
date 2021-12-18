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
    public class SearchController : ControllerBase
    {
        private SearchModel model;

        public SearchController(food_location_dbContext db)
        {
            model = new SearchModel(db);
        }

        [HttpPost]
        [Route("getRes_Distance")]
        public IActionResult getAllRestaurant(Object.Input.InputRes_distance location)
        {
            if(location.distance > 20 || location.distance < 5)
            {
                return Ok(new Object.Get.Message_ResList(2, "Phạm vi tìm kiếm từ 5km đến 20km", null, null, null));
            }

            var result = model.resList(new Other.LngLat(location.lon, location.lat), location.distance);

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithDistrict")]
        public IActionResult getAllRestaurantWithDistrict(Object.Input.InputRes_District area)
        {
            var result = model.resListWithDistrict(area.district, new Other.LngLat(area.lon, area.lat));          

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithSearch")]
        public IActionResult resListSearch(Object.Input.InputRes_Search input)
        {
            var result = model.resListSearch(input.name, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithSupperSearch")]
        public IActionResult resListSupperSearch(Object.Input.InputRes_Search input)
        {
            var result = model.resListSupperSearch(input.catelogyList, input.districtList,input.name, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithCategorys")]
        public IActionResult getResWithCategorys(Object.Input.InputRes_CategoryList input)
        {
            var result = model.resResWithCategorys(input.catelogyList, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithDistricts")]
        public IActionResult getResWithDistricts(Object.Input.InputRes_DistrictList input)
        {
            var result = model.resResWithDistricts(input.districtList, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithCategorysAndDistricts")]
        public IActionResult getResWithCategorysAndDistricts(Object.Input.InputRes_CategoryListAndDistrictList input)
        {
            var result = model.getResWithCategorysAndDistricts(input.catelogyList, input.districtList, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));
        }

        [HttpPost]
        [Route("getResWithNameOrAdd")]
        public IActionResult getResWithNameOrAdd(Object.Input.InputRes_ResNameOfAdd input)
        {
            var result = model.getResWithNameOrAdd(input.key, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null, null, null));
            }
            var result1 = model.categoryResList(result);
            var result2 = model.districtList(result);

            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result, result1, result2));

        }
        //[HttpPost]
        //[Route("getResWithFood")]
        //public IActionResult getAllRestaurantWithFood(Object.Input.InputRes_Food intput)
        //{
        //    var result = model.resListWithFood(intput.foodId, new Other.LngLat(intput.lon, intput.lat));

        //    if (result == null)
        //    {
        //        return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null));
        //    }
        //    return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result));
        //}
    }
}
