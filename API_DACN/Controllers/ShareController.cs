﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Controllers
{
    public class ShareController : Controller
    {
        public IActionResult Code(int id)
        {
            ViewBag.code = id;
            return View();
        }
    }
}
