namespace CartService.Cart.Responses
{
    public class UpdateCartItemsResponse
    {
        public int CartId { get; }

        public UpdateCartItemsResponse(int cartId)
        {
            CartId = cartId;
        }
    }
}