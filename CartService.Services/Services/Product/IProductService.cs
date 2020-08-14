using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartService.Services.Services.Product
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<Product>> GetByIds(IReadOnlyCollection<int> ids);
    }
}