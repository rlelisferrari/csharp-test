using System.Collections.Generic;

namespace WebApi.AuxClasses
{
    public class OrderRequest
    {
        public int UserId;
        public List<ProductRequest> ProductList { get; set; }
    }
}