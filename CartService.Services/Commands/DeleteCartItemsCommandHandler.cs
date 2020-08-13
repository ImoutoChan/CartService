using System.Threading;
using System.Threading.Tasks;
using CartService.DataAccess;
using MediatR;

namespace CartService.Services.Commands
{
    public class DeleteCartItemsCommandHandler : IRequestHandler<DeleteCartItemsCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public DeleteCartItemsCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Unit> Handle(DeleteCartItemsCommand request, CancellationToken cancellationToken)
        {
            await _cartItemRepository.Delete(request.Id, request.ProductIds);

            return Unit.Value;
        }
    }
}