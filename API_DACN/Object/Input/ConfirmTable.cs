using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Object.Input
{
    public class ConfirmTable
    {
        public string userId { get; set; }
        public string reserveTableId { set; get; }
        // status => 1: xác nhận || 0: từ chối
        public int status { get; set; }
    }
}
