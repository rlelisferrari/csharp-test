using System.Collections.Generic;

namespace DOMAIN.ODTClasses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public float TotalAmount { get; set; }
        public int UserId { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}