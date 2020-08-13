using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using MediatR;

namespace CartService.Services.Queries
{
    public class CartViewQueryHandler : IRequestHandler<CartViewQuery, CartView>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartViewQueryHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartView> Handle(CartViewQuery request, CancellationToken cancellationToken)
        {
            var products = await _cartItemRepository.Get(request.Id);

            return new CartView(request.Id, products);
        }
    }
}