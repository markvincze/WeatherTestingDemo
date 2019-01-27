using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherTestingDemo.Options;

namespace WeatherTestingDemo.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly OpenWeatherMapOptions options;

        public OpenWeatherMapService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherMapOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options.Value;
        }

        public async Task<OpenWeatherMapResponse> GetWeather(string query)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                var uriBuilder = new UriBuilder(options.BaseUrl)
                {
                    Path = "/data/2.5/weather",
                    Query = $"?q={query}&appid={options.ApiKey}"
                };

                var response = await httpClient.GetAsync(uriBuilder.Uri);

                return JsonConvert.DeserializeObject<OpenWeatherMapResponse>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
