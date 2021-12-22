using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class UserComment
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }

        public virtual Review Review { get; set; }
        public virtual User User { get; set; }
    }
}
