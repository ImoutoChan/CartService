using System;
using System.Net.Http;
using AutoMapper;
using CartService.DataAccess;
using CartService.DataAccess.Options;
using CartService.Infrastructure;
using CartService.Infrastructure.Quartz;
using CartService.Quartz;
using CartService.Services.Commands.Cart;
using CartService.Services.Queries;
using CartService.Services.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace CartService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // todo cleanup
            services.AddMediatR(typeof(CartViewQuery));
            services.AddTransient(typeof (IPipelineBehavior<,>), typeof (LoggingBehavior<,>));

            services.AddAutoMapper(typeof(WebApiProfile));

            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IWebHookRepository, WebHookRepository>();
            services.AddTransient<ICartServiceConnectionFactory, CartServiceConnectionFactory>();
            services.AddTransient<ICartService, Services.Services.CartService>();

            services
                .AddHttpClient<IWebHookCaller, WebHookCaller>()
                .AddPolicyHandler(GetRetryPolicy());

            services.AddOptions();
            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
            services.Configure<CleanupCartsSettings>(Configuration.GetSection(nameof(CleanupCartsSettings)));

            services.AddLogging(builder => builder.AddConsole());

            services.AddQuartzJob<CleanupCartsJob, CleanupCartsJob.Description>();


            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        "v1.0",
                        new OpenApiInfo()
                        {
                            Title = "CartService",
                            Version = "v1.0"
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "CartService API V1.0");
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
