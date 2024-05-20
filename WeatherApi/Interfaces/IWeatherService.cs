using WeatherApi.Models;

namespace WeatherApi.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeather(string city);
    }
}
