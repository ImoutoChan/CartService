using System;
using System.Net.Http;
using AutoMapper;
using CartService.DataAccess;
using CartService.DataAccess.Options;
using CartService.Infrastructure;
using CartService.Infrastructure.Quartz;
using CartService.Quartz;
using CartService.Services.Queries;
using CartService.Services.Services;
using CartService.Services.Services.Product;
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
            services.AddMediatR(typeof(CartViewQuery));
            services.AddTransient(typeof (IPipelineBehavior<,>), typeof (LoggingBehavior<,>));
            services.AddAutoMapper(typeof(WebApiProfile));
            services.AddLogging(builder => builder.AddConsole());

            AddDataAccess(services);
            AddServices(services);

            services.AddOptions();
            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
            services.Configure<QuartzSettings>(Configuration.GetSection(nameof(QuartzSettings)));

            services.AddQuartzJob<CleanupCartsJob, CleanupCartsJob.Description>();
            services.AddQuartzJob<GenerateReportsJob, GenerateReportsJob.Description>();

            AddSwagger(services);
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        "v1.0",
                        new OpenApiInfo
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

            app.UseMiddleware<ErrorLoggingMiddleware>();

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

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ICartService, Services.Services.CartService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReportService, ReportService>();

            services
                .AddHttpClient<IWebHookCaller, WebHookCaller>()
                .AddPolicyHandler(GetRetryPolicy());
        }

        private static void AddDataAccess(IServiceCollection services)
        {
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IWebHookRepository, WebHookRepository>();
            services.AddTransient<ICartServiceConnectionFactory, CartServiceConnectionFactory>();
        }
    }
}
