namespace CartService.Core
{
    public class CartItem
    {
        public int CartId { get; }

        public int ProductId { get; }

        public int Quantity { get; }

        public CartItem(int cartId, int productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}