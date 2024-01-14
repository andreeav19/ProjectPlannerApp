using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace taskarescu.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public String Get()
        {
            return "Test";
        }
    }
}