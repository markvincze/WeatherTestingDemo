using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace WeatherTestingDemo.IntegrationTests
{
    public class WeatherAlertsTests
    {
        [Fact]
        public async Task WeatherAlerts_Snow_SnowAlertReturned()
        {
            using (var server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>()))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("weatheralerts?query=Charlottetown");

                var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());

                Assert.Contains("snow", responseObject["alert"].Value<string>());
            }
        }
    }
}
