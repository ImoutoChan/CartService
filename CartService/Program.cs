using CartService.DataAccess.Migrations;
using CartService.Infrastructure.Quartz;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(
                        dispose: true, 
                        logger: CreateLogger());
                })
                .UseQuartz();

        private static Logger CreateLogger()
        {
            var loggerBuilder = new LoggerConfiguration()
                .Enrich.FromLogContext();

            loggerBuilder.MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] <s:{SourceContext}> {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.RollingFile(
                    pathFormat: "logs/{Date}-all.log",
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] <s:{SourceContext}> {Message}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Verbose);
            var logger = loggerBuilder.CreateLogger();
            return logger;
        }
    }
}
