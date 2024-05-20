using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using WeatherApi.DTOs;
using WeatherApi.Interfaces;
using WeatherApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherApi.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        public OpenWeatherMapService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            using (var client = new HttpClient())
            {
                var apiKey = _configuration["OpenWeatherMap:ApiKey"];
                var baseUrl = _configuration["OpenWeatherMap:BaseUrl"];
                var requestUrl = $"{baseUrl}?q={city}&appid={apiKey}&units=metric";
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var weatherResponse = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(responseContent);
                        return MapToWeatherData(weatherResponse);
                    }
                    catch (Exception ex)
                    {                        
                        throw new Exception($"Error deserializing weather data: {ex.Message}");                        
                    }
                }
                else
                {
                    throw new Exception($"API call failed: {response.ReasonPhrase}");                    
                }
            }
        } 

        private WeatherData MapToWeatherData(OpenWeatherMapResponse response)
        {
            var weatherData = new WeatherData
            {
                City = response.Name,
                Country = response.Sys.Country,
                FeelsLike = response.Main.Feels_Like,
                MaxTemp = response.Main.Temp_Max,
                MinTemp = response.Main.Temp_Min,
                Humidity = response.Main.Humidity,
                Icon = response.Weather[0].Icon,
                Description = response.Weather[0].Main,
                Temperature = response.Main.Temp,
                Sunrise = response.Sys.Sunrise,
                Sunset = response.Sys.Sunset,
                CurrentDate = response.Dt,
                WindSpeed = response.Wind.Speed
            };

            return weatherData;
        }                 
    }
}
