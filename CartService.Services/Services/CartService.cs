using System.Threading.Tasks;
using CartService.DataAccess;
using Microsoft.Extensions.Logging;

namespace CartService.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task ValidateCartId(int id)
        {
            var cart = await _cartRepository.Get(id);
            if (cart == null)
            {
                _logger.LogError("The cart with id {CartId} was not found.", id);
                throw new CartException($"The cart with id {id} was not found.");
            }
        }
    }
}