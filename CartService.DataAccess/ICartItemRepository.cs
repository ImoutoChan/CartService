using System.Collections.Generic;
using System.Threading.Tasks;
using CartService.Core;

namespace CartService.DataAccess
{
    public interface ICartItemRepository
    {
        Task<IReadOnlyCollection<CartItemEntry>> Get(int cartId);

        Task<IReadOnlyCollection<CartItem>> GetItems(IReadOnlyCollection<int> cartIds);

        Task Delete(int cartId, IReadOnlyCollection<int> productIds);

        Task<int> Add(int? cartId, IReadOnlyCollection<CartItemEntry> items);
    }
}