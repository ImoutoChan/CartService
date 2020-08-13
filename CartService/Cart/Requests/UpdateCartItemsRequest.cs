using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartService.Cart.Requests
{
    public class UpdateCartItemsRequest
    {
        public int? Id { get; set; }

        [Required]
        [MinLength(1)]
        public IReadOnlyCollection<CartItemEntryRequest> Products { get; set; } = default!;
    }
}