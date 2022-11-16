using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SimpleApiWithAzureVault.Models;
using System.Net.Http;

namespace SimpleApiWithAzureVault.Services
{
    public sealed class WeatherService : IWeatherService
    {
        private readonly ILogger<WeatherService> _logger = null!;
        private readonly IOptions<WeatherEndpointOptions> _configuration = null!;
        public WeatherService(ILogger<WeatherService> logger, IOptions<WeatherEndpointOptions> configuration)
            => (_logger, _configuration)=
            ( logger, configuration);
        
        public string GetWeatherForecast()
        {
            try
            {

                return _configuration.Value.apiKey;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return "Error";
            }
        }
    }
}
