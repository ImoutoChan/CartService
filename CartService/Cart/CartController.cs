using System.Threading.Tasks;
using AutoMapper;
using CartService.Cart.Requests;
using CartService.Cart.Responses;
using CartService.Services.Commands;
using CartService.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Cart
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<CartView> Get(int id)
        {
            return await _mediator.Send(new CartViewQuery(id));
        }

        [HttpPost]
        public async Task<UpdateCartItemsResponse> Update(UpdateCartItemsRequest request)
        {
            var command = _mapper.Map<AddCartItemsCommand>(request);
            var cartId = await _mediator.Send(command);

            return new UpdateCartItemsResponse(cartId);
        }

        [HttpDelete]
        public async Task Delete(DeleteCartItemsRequest request)
        {
            var command = _mapper.Map<DeleteCartItemsCommand>(request);
            await _mediator.Send(command);
        }
    }
}
