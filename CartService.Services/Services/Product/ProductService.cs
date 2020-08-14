using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Services.Services.Product
{
    public class ProductService : IProductService
    {
        public Task<IReadOnlyCollection<Product>> GetByIds(IReadOnlyCollection<int> ids)
        {
            // seed data instead of the real product service

            var products = GetAllProducts().ToDictionary(x => x.Id);

            var result = ids.Select(x => products[x]).ToList() as IReadOnlyCollection<Product>;

            return Task.FromResult(result);
        }

        private IEnumerable<Product> GetAllProducts()
        {
            yield return new Product(1, "Телефон", 50_000, false);
            yield return new Product(2, "Чайник", 1_000, false);
            yield return new Product(3, "Утюг", 5_000, true);
            yield return new Product(4, "Пылесос", 10_000, true);
            yield return new Product(5, "Гитара", 30_000, false);
            yield return new Product(6, "Микроволновка", 15_000, false);
            yield return new Product(7, "Микрофон", 7_000, false);
            yield return new Product(8, "Наукшники", 16_000, true);
        }
    }
}