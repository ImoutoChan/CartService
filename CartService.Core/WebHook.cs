using System;

namespace CartService.Core
{
    public class WebHook
    {
        public int Id { get; }

        public int CartId { get; }

        public string Uri { get; }

        public WebHook(int id, int cartId, string uri)
        {
            Id = id;
            CartId = cartId;
            Uri = uri;
        }
    }
}