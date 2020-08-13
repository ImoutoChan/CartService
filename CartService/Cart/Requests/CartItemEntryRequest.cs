namespace CartService.Cart.Requests
{
    public class CartItemEntryRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}