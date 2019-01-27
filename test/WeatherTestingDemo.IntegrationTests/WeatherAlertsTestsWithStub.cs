using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Stubbery;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WeatherTestingDemo.Options;
using Xunit;

namespace WeatherTestingDemo.IntegrationTests
{
    public class WeatherAlertsTestsWithStub
    {
        private const string snowResponse = @"
{
    ""weather"": [{
        ""id"": 600,
        ""main"": ""Snow"",
        ""description"": ""light snow""
    }]
}";

        private ApiStub StartStub()
        {
            var stub = new ApiStub();

            stub.Get(
                "/data/2.5/weather?q=TestCity&appid=testkey",
                (req, args) => snowResponse);

            stub.Start();
            return stub;
        }

        private TestServer StartTestServer(ApiStub stub)
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    services.Configure<OpenWeatherMapOptions>(opts =>
                    {
                        opts.BaseUrl = stub.Address;
                        opts.ApiKey = "testkey";
                    });
                });

            return new TestServer(builder);
        }

        [Fact]
        public async Task WeatherAlerts_Snow_SnowAlertReturned()
        {
            using (var stub = StartStub())
            using (var server = StartTestServer(stub))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("weatheralerts?query=TestCity");

                var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());

                Assert.Contains("snow", responseObject["alert"].Value<string>());
            }
        }
    }
}
