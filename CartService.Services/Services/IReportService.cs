namespace CartService.Services.Commands.Cart
{
    public interface IReportService
    {
        void GenerateReport(
            int totalCartsCount,
            int withBonusProductsCount,
            int expireIn10Days,
            int expireIn20Days,
            int expireIn30Days,
            decimal average);
    }
}