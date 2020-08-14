using MediatR;

namespace CartService.Services.Commands.WebHook
{
    public class AddWebHookCommand : IRequest
    {
        public int CartId { get; }

        public string WebHook { get; }

        public AddWebHookCommand(int cartId, string webHook)
        {
            CartId = cartId;
            WebHook = webHook;
        }
    }
}