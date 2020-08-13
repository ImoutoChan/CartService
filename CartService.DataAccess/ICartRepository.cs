using System;
using System.Collections.Generic;
using CartService.Core;

namespace CartService.DataAccess
{
    public interface ICartRepository
    {
        IReadOnlyCollection<Cart> GetOutdatedCarts(DateTimeOffset olderThan);

        Cart? GetCart(int id);

        void DeleteCarts(IReadOnlyCollection<int> cartIds);
    }
}