using System.Data;

namespace CartService.DataAccess.Options
{
    public interface ICartServiceConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}