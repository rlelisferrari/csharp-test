using System;
using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class Product : Base.Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<OrderContainsProduct> OrderContainsProducts { get; set; }
    }
}