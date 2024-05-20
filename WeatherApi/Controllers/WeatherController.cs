using Microsoft.AspNetCore.Mvc;
using WeatherApi.Interfaces;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet(Name = "weather")]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                var weatherDataList = await _weatherService.GetWeather(city);
                return Ok(weatherDataList);
            }
            catch (Exception ex)
            {
                return BadRequest("Error retrieving weather data.");
            }
        }
    }
}
