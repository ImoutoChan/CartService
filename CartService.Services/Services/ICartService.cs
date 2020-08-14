using System.Threading.Tasks;

namespace CartService.Services.Services
{
    public interface ICartService
    {
        Task ValidateCartId(int id);
    }
}