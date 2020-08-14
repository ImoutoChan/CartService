using System;
using System.IO;

namespace CartService.Services.Commands.Cart
{
    public class ReportService : IReportService
    {
        public void GenerateReport(
            int totalCartsCount,
            int withBonusProductsCount,
            int expireIn10Days,
            int expireIn20Days,
            int expireIn30Days,
            decimal average)
        {
            var filename = Path.GetRandomFileName() + ".txt";
            var content = string.Format(
                GetTemplate(),
                DateTimeOffset.Now,
                totalCartsCount,
                withBonusProductsCount,
                expireIn10Days,
                expireIn20Days,
                expireIn30Days,
                average);

            File.WriteAllText(filename, content);
        }

        private string GetTemplate()
            => "Report on Date: {0}\n" +
               "Total carts: {1}\n" +
               "Bonus points carts: {2}\n" +
               "Carts will expire by 10 20 30 days: {3} {4} {5}\n" +
               "Average cost per cart: {6}\n";
    }
}