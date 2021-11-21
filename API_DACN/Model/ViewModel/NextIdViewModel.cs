using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_DACN.Model.ViewModel
{
    public class NextIdViewModel
    {
        [Key]
        public string NextId { get; set; }
    }
}
