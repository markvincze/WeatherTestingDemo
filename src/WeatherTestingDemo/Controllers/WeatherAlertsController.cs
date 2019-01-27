using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherTestingDemo.Models;
using WeatherTestingDemo.Services;

namespace WeatherTestingDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherAlertsController : ControllerBase
    {
        private readonly IOpenWeatherMapService openWeatherMapService;

        public WeatherAlertsController(IOpenWeatherMapService openWeatherMapService)
        {
            this.openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            var weather = await openWeatherMapService.GetWeather(query);

            var alertWeather = weather.Weather.FirstOrDefault(w => w.Main == "Snow" || w.Main == "Thunderstorm");

            if (alertWeather == null)
            {
                return Ok(new WeatherAlert { Alert = null });
            }

            return Ok(new WeatherAlert { Alert = alertWeather.Main == "Snow" ? "It's snowing!" : "There is a storm!" });
        }
    }
}
