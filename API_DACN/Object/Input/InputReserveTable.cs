﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class InputReserveTable
    {
        public int quantity { get; set; }
        public string time { get; set; }
        public string restaurantId { get; set; }
        public string promotionId { get; set; }
        public string userId { get; set; }
    }
}
