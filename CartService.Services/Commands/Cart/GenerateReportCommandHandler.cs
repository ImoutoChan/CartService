using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CartService.Core;
using CartService.DataAccess;
using CartService.Services.Services;
using CartService.Services.Services.Product;
using MediatR;

namespace CartService.Services.Commands.Cart
{
    public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IReportService _reportService;
        private readonly IProductService _productService;

        public GenerateReportCommandHandler(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IReportService reportService,
            IProductService productService)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _reportService = reportService;
            _productService = productService;
        }

        public async Task<Unit> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.GetAll();
            var cartIds = carts.Select(x => x.Id).ToArray();

            var cartProducts = await _cartItemRepository.GetItems(cartIds);
            var products = await _productService.GetByIds(cartProducts.Select(x => x.ProductId).Distinct().ToArray());
            var productsDict = products.ToDictionary(x => x.Id);
            

            var totalCartsCount = carts.Count;
            var withBonusProductsCount = GetWithBonusProductsCount(cartProducts, productsDict);
            var expireIn10Days = GetExpireBy(carts, DateTimeOffset.Now.AddDays(10));
            var expireIn20Days = GetExpireBy(carts, DateTimeOffset.Now.AddDays(20));
            var expireIn30Days = GetExpireBy(carts, DateTimeOffset.Now.AddDays(30));
            var average = CalculateAverageCost(cartProducts, productsDict);

            _reportService.GenerateReport(
                totalCartsCount,
                withBonusProductsCount,
                expireIn10Days,
                expireIn20Days,
                expireIn30Days,
                average);

            return Unit.Value;
        }

        private static decimal CalculateAverageCost(
            IReadOnlyCollection<CartItem> cartProducts, 
            IReadOnlyDictionary<int, Product> products)
        {
            return cartProducts
                .GroupBy(x => x.CartId)
                .Select(x => x.Sum(y => products[y.ProductId].Cost * y.Quantity))
                .Average();
        }

        private static int GetExpireBy(IReadOnlyCollection<Core.Cart> carts, DateTimeOffset expireBy) 
            => carts.Count(x => x.Created < expireBy.AddDays(-29));

        private int GetWithBonusProductsCount(
            IReadOnlyCollection<CartItem> cartProducts,
            IReadOnlyDictionary<int, Product> products)
        {
            return cartProducts
                .GroupBy(x => x.CartId)
                .Count(x => x.Any(y => products[y.ProductId].ForBonusPoints));
        }
    }
}