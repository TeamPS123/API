using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Review
    {
        public Review()
        {
            UserComments = new HashSet<UserComment>();
            UserLikes = new HashSet<UserLike>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public int Value { get; set; }
        public string Date { get; set; }
        public long? CountLike { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }
        public virtual ICollection<UserLike> UserLikes { get; set; }
    }
}
