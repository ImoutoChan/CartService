using CartService.DataAccess.Migrations;
using CartService.Infrastructure.Quartz;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CartService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MigrationsRunner.SetupDatabase();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseQuartz();
    }
}
