namespace CartService.Core
{
    public class CartItemEntry
    {
        public int ProductId { get; }

        public int Quantity { get; }

        public CartItemEntry(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}