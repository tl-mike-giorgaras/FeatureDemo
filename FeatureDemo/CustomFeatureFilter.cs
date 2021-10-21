using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace FeatureDemo
{
    [FilterAlias("Custom")] // How we will refer to the filter in configuration
    public class CustomFeatureFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var allowedAccept = context.Parameters.GetValue<string>("AllowedAccept");
            var actualAccept = _httpContextAccessor.HttpContext?.Request.Headers["Accept"];

            return Task.FromResult(allowedAccept.Split(",").Contains(actualAccept.ToString()));
        }
    }
}