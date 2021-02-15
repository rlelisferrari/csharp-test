using System.Threading.Tasks;
using DATA.Repositories;

namespace DOMAIN.Interfaces.Repositories
{
    public interface IOrderContainsProductRepository
    {
        Task<OrderContainsProduct> AddAsyn(OrderContainsProduct orderContainsProduct);
        void Dispose();
    }
}