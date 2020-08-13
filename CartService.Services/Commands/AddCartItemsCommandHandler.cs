using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CartService.Core;
using CartService.DataAccess;
using MediatR;

namespace CartService.Services.Commands
{
    public class AddCartItemsCommandHandler : IRequestHandler<AddCartItemsCommand, int>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public AddCartItemsCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<int> Handle(AddCartItemsCommand request, CancellationToken cancellationToken)
        {
            var products = request
                .Products
                .Select(x => new CartItemEntry(x.ProductId, x.Quantity))
                .ToArray();

            return await _cartItemRepository.Add(request.Id, products);
        }
    }
}