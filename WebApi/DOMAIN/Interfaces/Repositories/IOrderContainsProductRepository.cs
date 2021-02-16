using System.Threading.Tasks;
using DOMAIN.Models;

namespace DOMAIN.Interfaces.Repositories
{
    public interface IOrderContainsProductRepository
    {
        Task<OrderContainsProduct> AddAsyn(OrderContainsProduct orderContainsProduct);
        void Dispose();
    }
}