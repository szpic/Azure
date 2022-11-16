using Microsoft.AspNetCore.Mvc;
using SimpleApiWithAzureVault.Models;
using SimpleApiWithAzureVault.Services;

namespace SimpleApiWithAzureVault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weather, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weather; 
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return _weatherService.GetWeatherForecast();
        }
    }
}