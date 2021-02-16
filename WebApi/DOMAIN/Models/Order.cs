using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class Order : Base.Base
    {
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public ICollection<OrderContainsProduct> OrderContainsProducts { get; set; }
    }
}