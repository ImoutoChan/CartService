using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.DataAccess.Options;
using Dapper;

namespace CartService.DataAccess
{
    public class WebHookRepository : IWebHookRepository
    {
        private readonly ICartServiceConnectionFactory _cartServiceConnectionFactory;

        public WebHookRepository(ICartServiceConnectionFactory cartServiceConnectionFactory)
        {
            _cartServiceConnectionFactory = cartServiceConnectionFactory;
        }

        public async Task Add(int cartId, string webHook)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();
            
            await connection.ExecuteAsync(
                "INSERT INTO WebHook(CartId, Uri) " +
                "VALUES (@cartId, @uri)",
                new {cartId, uri = webHook});
        }

        public async Task Delete(int cartId, string webHook)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            await connection.QueryAsync(
                "DELETE FROM WebHook " +
                "WHERE CartId = @cartId AND Uri = @uri",
                new {cartId, uri = webHook});
        }

        public async Task<IReadOnlyCollection<string>> GetForCarts(IReadOnlyCollection<int> cartIds)
        {
            using var connection = _cartServiceConnectionFactory.CreateConnection();

            return (await connection.QueryAsync<string>(
                "SELECT Uri " +
                "FROM WebHook " +
                "WHERE CartId IN @cartIds",
                new {cartIds}))
                .ToList();
        }
    }
}