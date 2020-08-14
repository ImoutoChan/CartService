using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CartService.Core;

namespace CartService.DataAccess
{
    public interface ICartRepository
    {
        Task<IReadOnlyCollection<Cart>> GetOutdated(DateTimeOffset olderThan);

        Task<Cart?> Get(int id);

        Task Delete(IReadOnlyCollection<int> cartIds);

        Task<IReadOnlyCollection<Cart>> GetAll();
    }
}