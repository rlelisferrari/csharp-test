using System.Collections.Generic;
using DATA.Repositories;

namespace DOMAIN.Models
{
    public class Order : Base.Base
    {
        public int UserId { get; set; }
        public ICollection<OrderContainsProduct> OrderContainsProducts { get; set; }
    }
}