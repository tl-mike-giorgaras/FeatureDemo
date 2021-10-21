using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FeatureDemo
{
    public class RewritingMiddleware
    {
        private readonly RequestDelegate _next;

        public RewritingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "application/text";
            await context.Response.WriteAsync("Overwritten by middleware");
        }
    }
}