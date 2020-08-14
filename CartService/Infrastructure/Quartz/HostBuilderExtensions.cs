using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartService.Infrastructure.Quartz
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseQuartz(
            this IHostBuilder hostBuilder, 
            Action<IServiceCollection>? jobsBuilder = null)
        {
            hostBuilder
                .ConfigureServices(
                    (context, collection)
                        =>
                    {
                        collection
                            .AddQuartz()
                            .AddHostedService<QuartzHostedService>();

                        jobsBuilder?.Invoke(collection);
                    });

            return hostBuilder;
        }
    }
}