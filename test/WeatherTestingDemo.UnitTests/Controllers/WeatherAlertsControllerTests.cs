using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WeatherTestingDemo.Controllers;
using WeatherTestingDemo.Models;
using WeatherTestingDemo.Services;
using Xunit;

namespace WeatherTestingDemo.UnitTests.Controllers
{
    public class WeatherAlertsControllerTests
    {
        [Fact]
        public async Task Get_Snow_SnowAlertReturned()
        {
            var serviceMock = new Mock<IOpenWeatherMapService>();

            serviceMock.Setup(s => s.GetWeather("TestQuery")).ReturnsAsync(
                new OpenWeatherMapResponse
                {
                    Weather = new[]
                    {
                        new OpenWeatherMapResponseWeather
                        {
                            Main = "Snow"
                        }
                    }
                });

            var sut = new WeatherAlertsController(serviceMock.Object);

            // Act
            var result = await sut.Get("TestQuery");

            Assert.IsType<OkObjectResult>(result);

            var resultObject = ((OkObjectResult)result).Value;

            Assert.IsType<WeatherAlert>(resultObject);

            Assert.NotNull(((WeatherAlert)resultObject).Alert);
        }
    }
}
