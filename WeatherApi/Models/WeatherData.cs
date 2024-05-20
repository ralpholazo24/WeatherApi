namespace WeatherApi.Models
{
    public class WeatherData
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int Humidity { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public long Sunset { get; set; }
        public long Sunrise { get; set; }
        public long CurrentDate { get; set; }
        public double WindSpeed { get; set; }
    }
}
