using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CartService.DataAccess.Options
{
    public class CartServiceConnectionFactory : ICartServiceConnectionFactory
    {
        private readonly string _connectionString;

        public CartServiceConnectionFactory(IOptionsSnapshot<ConnectionStrings> options)
        {
            _connectionString = options.Value.CartServiceMsSql;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}