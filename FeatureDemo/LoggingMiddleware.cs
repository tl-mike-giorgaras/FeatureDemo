using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace FeatureDemo
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IFeatureManager _featureManager;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger,
            IFeatureManager featureManager)
        {
            _next = next;
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var featureValues =Enum.GetValues(typeof(FeatureFlags)).Cast<FeatureFlags>()
                .ToDictionary(
                    x => x.ToString(), 
                    async x => await _featureManager.IsEnabledAsync(x.ToString()));


            using (_logger.BeginScope(featureValues))
            {
                await _next.Invoke(context);
            }
        }
    }
}