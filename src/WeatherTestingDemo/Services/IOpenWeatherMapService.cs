using System.Threading.Tasks;

namespace WeatherTestingDemo.Services
{
    public interface IOpenWeatherMapService
    {
        Task<OpenWeatherMapResponse> GetWeather(string query);
    }
}