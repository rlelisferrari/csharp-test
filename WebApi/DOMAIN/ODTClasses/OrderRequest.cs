using System.Collections.Generic;

namespace DOMAIN.ODTClasses
{
    public class OrderRequest
    {
        public int UserId;
        public List<ProductRequest> ProductList { get; set; }
    }
}