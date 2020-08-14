namespace CartService.Cart.Requests
{
    public class WebHookRequest
    {
        public int CartId { get; set; }

        public string WebHook { get; set; } = default!;
    }
}