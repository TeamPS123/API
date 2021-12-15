using API_DACN.Database;
using API_DACN.Model;
using API_DACN.Other;
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
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _HostEnvironment;
        private Other.Token Token;
        private ImageModel imageModel;

        public ImageController(IHostingEnvironment h, IConfiguration config, food_location_dbContext db)
        {
            Token = new Other.Token(config, db);
            this._HostEnvironment = h;
            imageModel = new ImageModel(db);
        }

        //[Route("upImage")]
        //[HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        //public IActionResult UpImage(string foodId, string restaurantId, string userId, string categoryId)
        //{
        //    if (Token.GetPhoneWithToken(Request.Headers) != userId)
        //    {
        //        return Ok(new Object.Message(2, "Kiểm tra lại token tý nào"));
        //    }

        //    string contentRootPath = _HostEnvironment.ContentRootPath;

        //    try
        //    {
        //        var photo = HttpContext.Request.Form.Files;
        //        foreach (var item in photo)
        //        {
        //            var imaageSavePath = Path.Combine(contentRootPath, "Picture", item.FileName);
        //            if (!System.IO.File.Exists(imaageSavePath))
        //            {
        //                imageModel.AddImage(item.FileName, foodId, restaurantId, userId, categoryId);
        //                var stream = System.IO.File.Create(imaageSavePath);
        //                item.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //    catch( Exception ex)
        //    {
        //        return Ok(new Object.Message(0, ex+""/*"Thêm ảnh thất bại"*/));
        //    }
        //    return Ok(new Object.Message(1, "Thêm ảnh thành công"));
        //}

        [Route("upImageOfUser")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult UpImageOfUser(string userId)  
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            string contentRootPath = _HostEnvironment.ContentRootPath;

            try
            {
                var photo = HttpContext.Request.Form.Files;
                foreach (var item in photo)
                {
                    string[] name = item.FileName.Split(".");
                    var imaageSavePath = Path.Combine(contentRootPath, "Picture", name[0]+"_"+userId+"."+name[1]);
                    if (!System.IO.File.Exists(imaageSavePath))
                    {
                        var result = imageModel.AddImageOfUser(name[0] + "_" + userId + "." + name[1], userId, imaageSavePath);
                        if (!result)
                        {
                            return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
                        }
                        var stream = System.IO.File.Create(imaageSavePath);
                        item.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm ảnh thành công", null));
        }

        [Route("upImageOfRes")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult UpImageOfRes(string userId, string restaurantId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            string contentRootPath = _HostEnvironment.ContentRootPath;

            try
            {
                var photo = HttpContext.Request.Form.Files;
                foreach (var item in photo)
                {
                    string[] name = item.FileName.Split(".");
                    var imaageSavePath = Path.Combine(contentRootPath, "Picture", name[0] + "_" + restaurantId + "." + name[1]);
                    if (!System.IO.File.Exists(imaageSavePath))
                    {
                        var result = imageModel.AddImageOfRes(name[0] + "_" + restaurantId + "." + name[1], userId, restaurantId, imaageSavePath);
                        if (!result)
                        {
                            return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
                        }
                        var stream = System.IO.File.Create(imaageSavePath);
                        item.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm ảnh thành công", null));
        }

        [Route("upImageOfFood")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult UpImageOfFood(string userId, string restaurantId, string foodId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            string contentRootPath = _HostEnvironment.ContentRootPath;
            string link = "";

            try
            {
                var photo = HttpContext.Request.Form.Files;
                foreach (var item in photo)
                {
                    string[] name = item.FileName.Split(".");
                    var imaageSavePath = Path.Combine(contentRootPath, "Picture", name[0] + "_" + foodId + "." + name[1]);
                    if (!System.IO.File.Exists(imaageSavePath))
                    {
                        link = imageModel.AddImageOfFood(name[0] + "_" + foodId + "." + name[1], userId, restaurantId, foodId, imaageSavePath);
                        if (link == "null")
                        {
                            return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
                        }
                        var stream = System.IO.File.Create(imaageSavePath);
                        item.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm ảnh thành công", link));
        }

        [Route("upImageOfRate")]
        [HttpPost]
        [RequestFormSizeLimit(valueCountLimit: 104857600)]
        public IActionResult upImageOfRate(string userId, int rateId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            string contentRootPath = _HostEnvironment.ContentRootPath;
            string link = "";

            try
            {
                var photo = HttpContext.Request.Form.Files;
                foreach (var item in photo)
                {
                    string[] name = item.FileName.Split(".");
                    var imaageSavePath = Path.Combine(contentRootPath, "Picture", name[0] + "_Rate" + rateId + "." + name[1]);
                    if (!System.IO.File.Exists(imaageSavePath))
                    {
                        link = imageModel.AddImageOfRate(name[0] + "_Rate" + rateId + "." + name[1], rateId, imaageSavePath);
                        if (link == "null")
                        {
                            return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
                        }
                        var stream = System.IO.File.Create(imaageSavePath);
                        item.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm ảnh thành công", link));
        }

        [Route("upImageOfReview")]
        [HttpPost]
        [RequestFormSizeLimit(valueCountLimit: 104857600)]
        public IActionResult upImageOfReview(string userId, int reviewId)
        {
            if (Token.GetPhoneWithToken(Request.Headers) != userId)
            {
                return Ok(new Object.Message(2, "Kiểm tra lại token tý nào", null));
            }

            string contentRootPath = _HostEnvironment.ContentRootPath;
            string link = "";

            try
            {
                var photo = HttpContext.Request.Form.Files;
                foreach (var item in photo)
                {
                    string[] name = item.FileName.Split(".");
                    var imaageSavePath = Path.Combine(contentRootPath, "Picture", name[0] + "_Review" + reviewId + "." + name[1]);
                    if (!System.IO.File.Exists(imaageSavePath))
                    {
                        link = imageModel.AddImageOfReview(name[0] + "_Review" + reviewId + "." + name[1], reviewId, imaageSavePath);
                        if (link == "null")
                        {
                            return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
                        }
                        var stream = System.IO.File.Create(imaageSavePath);
                        item.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                return Ok(new Object.Message(0, "Thêm ảnh thất bại", null));
            }
            return Ok(new Object.Message(1, "Thêm ảnh thành công", link));
        }
    }
}
