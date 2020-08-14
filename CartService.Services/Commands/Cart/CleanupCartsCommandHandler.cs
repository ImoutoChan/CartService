using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using MediatR;

namespace CartService.Services.Commands.Cart
{
    public class CleanupCartsCommandHandler : IRequestHandler<CleanupCartsCommand>
    {
        private readonly ICartRepository _cartRepository;

        public CleanupCartsCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Unit> Handle(CleanupCartsCommand request, CancellationToken cancellationToken)
        {
            var outdatedCarts = await _cartRepository.GetOutdatedCarts(DateTimeOffset.Now.AddDays(-30));
            await _cartRepository.DeleteCarts(outdatedCarts.Select(x => x.Id).ToArray());

            return Unit.Value;
        }
    }
}