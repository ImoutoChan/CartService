using System;

namespace CartService.Core
{
    public class Cart
    {
        public int Id { get; }

        public DateTimeOffset Created { get; }

        public DateTimeOffset Updated { get; }

        public Cart(
            int id,
            DateTimeOffset created,
            DateTimeOffset updated)
        {
            Id = id;
            Created = created;
            Updated = updated;
        }
    }
}