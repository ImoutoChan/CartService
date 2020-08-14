using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using CartService.Services.Services;
using MediatR;

namespace CartService.Services.Commands.Cart
{
    public class CleanupCartsCommandHandler : IRequestHandler<CleanupCartsCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IWebHookCaller _webHookCaller;
        private readonly IWebHookRepository _webHookRepository;

        public CleanupCartsCommandHandler(
            ICartRepository cartRepository,
            IWebHookRepository webHookRepository,
            IWebHookCaller webHookCaller)
        {
            _cartRepository = cartRepository;
            _webHookRepository = webHookRepository;
            _webHookCaller = webHookCaller;
        }

        public async Task<Unit> Handle(CleanupCartsCommand request, CancellationToken cancellationToken)
        {
            var outdatedCarts = await _cartRepository.GetOutdated(DateTimeOffset.Now.AddDays(-29));
            var cartIds = outdatedCarts.Select(x => x.Id).ToArray();

            var webHooks = await _webHookRepository.GetForCarts(cartIds);
            await _webHookCaller.Call(webHooks);

            await _cartRepository.Delete(cartIds);
            return Unit.Value;
        }
    }
}