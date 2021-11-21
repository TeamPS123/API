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
                return Ok(new Object.Get.Message_ResList(2, "Phạm vi tìm kiếm từ 5km đến 20km", null));
            }

            var result = model.resList(new Other.LngLat(location.lon, location.lat), location.distance);

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpPost]
        [Route("getResWithDistrict")]
        public IActionResult getAllRestaurantWithDistrict(Object.Input.InputRes_District area)
        {
            var result = model.resListWithDistrict(area.district, new Other.LngLat(area.lon, area.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result));
        }

        [HttpPost]
        [Route("getResWithSearch")]
        public IActionResult resListSearch(Object.Input.InputRes_Search input)
        {
            var result = model.resListSearch(input.name, new Other.LngLat(input.lon, input.lat));

            if (result == null)
            {
                return Ok(new Object.Get.Message_ResList(0, "Lấy dữ liệu thất bại", null));
            }
            return Ok(new Object.Get.Message_ResList(1, "Lấy dữ liệu thành công", result));
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
