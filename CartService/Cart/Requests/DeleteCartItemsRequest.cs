using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartService.Cart.Requests
{
    public class DeleteCartItemsRequest
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public IReadOnlyCollection<int> ProductIds { get; set; } = default!;
    }
}