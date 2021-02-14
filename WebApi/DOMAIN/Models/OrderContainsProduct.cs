using DOMAIN.Models;
using DOMAIN.Models.Base;

namespace DATA.Repositories
{
    public class OrderContainsProduct : Base
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}