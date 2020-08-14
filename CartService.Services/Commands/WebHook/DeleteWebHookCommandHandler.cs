using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using CartService.Services.Services;
using MediatR;

namespace CartService.Services.Commands.WebHook
{
    public class DeleteWebHookCommandHandler : IRequestHandler<DeleteWebHookCommand>
    {
        private readonly ICartService _cartService;
        private readonly IWebHookRepository _webHookRepository;

        public DeleteWebHookCommandHandler(
            IWebHookRepository webHookRepository,
            ICartService cartService)
        {
            _webHookRepository = webHookRepository;
            _cartService = cartService;
        }

        public async Task<Unit> Handle(DeleteWebHookCommand request, CancellationToken cancellationToken)
        {
            await _cartService.ValidateCartId(request.CartId);
            await _webHookRepository.Delete(request.CartId, request.WebHook);

            return Unit.Value;
        }
    }
}