using System;
using System.Collections.Generic;

#nullable disable

namespace API_DACN.Database
{
    public partial class Category
    {
        public Category()
        {
            Foods = new HashSet<Food>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public string KeyWord { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
