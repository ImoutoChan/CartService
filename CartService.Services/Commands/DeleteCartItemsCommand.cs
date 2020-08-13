using System.Collections.Generic;
using MediatR;

namespace CartService.Services.Commands
{
    public class DeleteCartItemsCommand : IRequest<Unit>
    {
        public int Id { get; }

        public IReadOnlyCollection<int> ProductIds { get; }

        public DeleteCartItemsCommand(int id, IReadOnlyCollection<int> productIds)
        {
            Id = id;
            ProductIds = productIds;
        }
    }
}