using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Core;
using CartService.DataAccess.Options;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CartService.DataAccess
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ICartServiceConnectionFactory _cartServiceConnectionFactory;
        private readonly ILogger _logger;

        public CartItemRepository(
            ICartServiceConnectionFactory cartServiceConnectionFactory, 
            ILogger<CartItemRepository> logger)
        {
            _cartServiceConnectionFactory = cartServiceConnectionFactory;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<CartItemEntry>> Get(int cartId)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            var result = await connection.QueryAsync<CartItemEntry>(
                "SELECT ProductId, Quantity " +
                "FROM CartItem " +
                "WHERE CartId = @cartId",
                new {cartId});

            return result.ToList();
        }

        public async Task Delete(int cartId, IReadOnlyCollection<int> productIds)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            await connection.QueryAsync(
                "DELETE FROM CartItem " +
                "WHERE CartId = @cartId " +
                "AND ProductId in @productIds",
                new {cartId, productIds});
        }

        public async Task<int> Add(int? possibleCartId, IReadOnlyCollection<CartItemEntry> products)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // create a new cart
                var cartId = possibleCartId ?? await connection.QuerySingleAsync<int>(
                    "INSERT INTO Cart(Created, Updated) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (GETDATE(), GETDATE())",
                    transaction: transaction);

                var productDict = products.ToDictionary(x => x.ProductId, x => x.Quantity);
            
                // get new product ids
                var existingProductIds = (await connection.QueryAsync<int>(
                        "SELECT ProductId " +
                        "FROM CartItem " +
                        "WHERE CartId = @cartId " +
                        "AND ProductId IN @productIds",
                        new {cartId, productIds = productDict.Keys},
                        transaction))
                    .ToList();

                // insert new products
                var newProducts = products
                    .Where(x => !existingProductIds.Contains(x.ProductId))
                    .Select(x => new {cartId, productId = x.ProductId, quantity = productDict[x.ProductId]});

                if (newProducts.Any())
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO CartItem(CartId, ProductId, Quantity) " +
                        "VALUES (@cartId, @productId, @quantity)",
                        newProducts,
                        transaction);
                }

                var existingProducts = products
                    .Where(x => existingProductIds.Contains(x.ProductId))
                    .Select(x => new {cartId, productId = x.ProductId, quantity = x.Quantity});

                if (existingProducts.Any())
                {
                    // update existing products
                    await connection.ExecuteAsync(
                        "UPDATE CartItem " +
                        "SET Quantity = @quantity " +
                        "WHERE CartId = @cartId AND ProductId = @productId",
                        existingProducts,
                        transaction);
                }

                transaction.Commit();

                return cartId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}