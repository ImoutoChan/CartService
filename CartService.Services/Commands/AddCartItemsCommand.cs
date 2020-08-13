using System.Collections.Generic;
using CartService.Core;
using MediatR;

namespace CartService.Services.Commands
{
    public class AddCartItemsCommand : IRequest<int>
    {
        public int? Id { get; }

        public IReadOnlyCollection<CartItemEntry> Products { get; }

        public AddCartItemsCommand(int? id, IReadOnlyCollection<CartItemEntry> products)
        {
            Id = id;
            Products = products;
        }
    }
}