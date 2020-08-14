using MediatR;

namespace CartService.Services.Commands.WebHook
{
    public class DeleteWebHookCommand : IRequest
    {
        public int CartId { get; }

        public string WebHook { get; }

        public DeleteWebHookCommand(int cartId, string webHook)
        {
            CartId = cartId;
            WebHook = webHook;
        }
    }
}