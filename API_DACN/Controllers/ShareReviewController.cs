using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    public class ShareReviewController : Controller
    {
        public IActionResult Code(int code)
        {
            return View();
        }
    }
}
