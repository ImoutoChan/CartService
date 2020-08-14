using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CartService.Core;
using CartService.DataAccess;
using CartService.Services.Services;
using MediatR;

namespace CartService.Services.Commands.Cart
{
    public class AddCartItemsCommandHandler : IRequestHandler<AddCartItemsCommand, int>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartService _cartService;

        public AddCartItemsCommandHandler(ICartItemRepository cartItemRepository, ICartService cartService)
        {
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
        }

        public async Task<int> Handle(AddCartItemsCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
            {
                await _cartService.ValidateCartId(request.Id.Value);
            }

            var products = request
                .Products
                .Select(x => new CartItemEntry(x.ProductId, x.Quantity))
                .ToArray();

            return await _cartItemRepository.Add(request.Id, products);
        }
    }
}