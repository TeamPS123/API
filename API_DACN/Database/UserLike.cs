using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class UserLike
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public bool Status { get; set; }

        public virtual Review Review { get; set; }
        public virtual User User { get; set; }
    }
}
