using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Core;
using CartService.DataAccess.Options;
using Dapper;

namespace CartService.DataAccess
{
    public class CartRepository : ICartRepository
    {
        private readonly ICartServiceConnectionFactory _cartServiceConnectionFactory;

        public CartRepository(ICartServiceConnectionFactory cartServiceConnectionFactory)
        {
            _cartServiceConnectionFactory = cartServiceConnectionFactory;
        }

        public async Task<IReadOnlyCollection<Cart>> GetOutdated(DateTimeOffset olderThan)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            var carts = await connection.QueryAsync<Cart>(
                "SELECT Id, Created, Updated " +
                "FROM Cart " +
                "WHERE Created < @olderThan", 
                new {olderThan = olderThan.DateTime});

            return carts.ToArray();
        }

        public async Task<Cart?> Get(int id)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Cart>(
                "SELECT Id, Created, Updated " +
                "FROM Cart " +
                "WHERE Id = @id",
                new {id});
        }

        public async Task Delete(IReadOnlyCollection<int> cartIds)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            await connection.QueryAsync(
                "DELETE FROM Cart " +
                "WHERE Id in @cartIds",
                new {cartIds});
        }

        public async Task<IReadOnlyCollection<Cart>> GetAll()
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            return (await connection.QueryAsync<Cart>(
                "SELECT Id, Created, Updated " +
                "FROM Cart "))
                .ToArray();
        }
    }
}