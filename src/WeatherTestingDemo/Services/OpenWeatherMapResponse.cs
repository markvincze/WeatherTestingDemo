namespace WeatherTestingDemo.Services
{
    public class OpenWeatherMapResponse
    {
        public OpenWeatherMapResponseWeather[] Weather { get; set; }
    }

    public class OpenWeatherMapResponseWeather
    {
        public int Id { get; set; }

        public string Main { get; set; }

        public string Description { get; set; }
    }
}
