using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartService.Services.Services
{
    public interface IWebHookCaller
    {
        Task Call(IReadOnlyCollection<string> webHooks);
    }
}