using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.DataAccess.Migrations
{
    public class MigrationsRunner
    {
        public static void SetupDatabase()
        {
            var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString("Server=localhost;Database=CartServiceDb;Trusted_Connection=True;")
                    .ScanIn(typeof(InitializeDatabase).Assembly)
                    .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            //runner.MigrateDown(20200808184900);
            runner.MigrateUp();
        }
    }
}