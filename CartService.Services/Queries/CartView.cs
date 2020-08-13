using System.Collections.Generic;
using CartService.Core;

namespace CartService.Services.Queries
{
    public class CartView
    {
        public int Id { get; }

        public IReadOnlyCollection<CartItemEntry> Products { get; }

        public CartView(int id, IReadOnlyCollection<CartItemEntry> products)
        {
            Id = id;
            Products = products;
        }
    }
}