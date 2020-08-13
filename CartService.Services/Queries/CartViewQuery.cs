using MediatR;

namespace CartService.Services.Queries
{
    public class CartViewQuery : IRequest<CartView>
    {
        public int Id { get; }

        public CartViewQuery(in int id)
        {
            Id = id;
        }
    }
}