using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeatureController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet("simple")]
        [FeatureGate(FeatureFlags.SimpleFilter)]
        public string GetSimple()
        {
            return "Hello world!";
        }
        
        [HttpGet("percentage")]
        public async Task<string> GetPercentage()
        {
            var enabled = await _featureManager.IsEnabledAsync(FeatureFlags.PercentageFilter.ToString());
            return $"Percentage filter is {(enabled?"on":"off")}";
        }
        
        [HttpGet("custom")]
        public async Task<string> GetCustom()
        {
            var enabled = await _featureManager.IsEnabledAsync(FeatureFlags.CustomFilter.ToString());
            return $"Custom filter is {(enabled?"on":"off")}";
        }
    }
}