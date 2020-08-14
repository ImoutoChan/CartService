using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CartService.Services.Services
{
    public class WebHookCaller : IWebHookCaller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WebHookCaller> _logger;

        public WebHookCaller(HttpClient httpClient, ILogger<WebHookCaller> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task Call(IReadOnlyCollection<string> webHooks)
        {
            foreach (var batch in webHooks.Batch(10))
            {
                var batchTasks = batch.Select(CallWebHook).ToList();
                await Task.WhenAll(batchTasks);
            }
        }

        private async Task CallWebHook(string uri)
        {
            try
            {
                await _httpClient.GetAsync(uri);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "The WebHook {WebHook} is not reachable.", uri);
            }
        }
    }
}