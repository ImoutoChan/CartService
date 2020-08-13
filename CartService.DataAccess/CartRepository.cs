using System;
using System.Collections.Generic;
using CartService.Core;

namespace CartService.DataAccess
{
    public class CartRepository : ICartRepository
    {
        public IReadOnlyCollection<Cart> GetOutdatedCarts(DateTimeOffset olderThan)
        {
            throw new NotImplementedException();
        }

        public Cart? GetCart(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCarts(IReadOnlyCollection<int> cartIds)
        {
            throw new NotImplementedException();
        }
    }
}