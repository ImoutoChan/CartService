using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CartService.Core;

namespace CartService.DataAccess
{
    public interface ICartRepository
    {
        Task<IReadOnlyCollection<Cart>> GetOutdatedCarts(DateTimeOffset olderThan);

        Task<Cart?> GetCart(int id);

        Task DeleteCarts(IReadOnlyCollection<int> cartIds);
    }
}