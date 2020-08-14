using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using CartService.Services.Services;
using MediatR;

namespace CartService.Services.Commands.WebHook
{
    public class AddWebHookCommandHandler : IRequestHandler<AddWebHookCommand>
    {
        private readonly ICartService _cartService;
        private readonly IWebHookRepository _webHookRepository;

        public AddWebHookCommandHandler(ICartService cartService, IWebHookRepository webHookRepository)
        {
            _cartService = cartService;
            _webHookRepository = webHookRepository;
        }

        public async Task<Unit> Handle(AddWebHookCommand request, CancellationToken cancellationToken)
        {
            await _cartService.ValidateCartId(request.CartId);
            await _webHookRepository.Add(request.CartId, request.WebHook);

            return Unit.Value;
        }
    }
}