using System.Threading.Tasks;
using AutoMapper;
using CartService.Cart.Requests;
using CartService.Services.Commands.WebHook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Cart
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WebHookController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task Add(WebHookRequest request)
        {
            var command = _mapper.Map<AddWebHookCommand>(request);
            await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task Delete(WebHookRequest request)
        {
            var command = _mapper.Map<DeleteWebHookCommand>(request);
            await _mediator.Send(command);
        }
    }
}