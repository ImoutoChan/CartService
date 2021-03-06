﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartService.DataAccess
{
    public interface IWebHookRepository
    {
        Task Add(int cartId, string webHook);

        Task Delete(int cartId, string webHook);

        Task<IReadOnlyCollection<string>> GetForCarts(IReadOnlyCollection<int> cartIds);
    }
}