using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLife.Blazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyLife.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            string responseContent = "Empty";
            using (HttpClient httpClient = new HttpClient())
            {
                var url = $"https://test01.azure-api.cn/echo/resource?param1=sample";
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
                httpRequest.Headers.Add("Accept", "application/json, text/plain, */*");

                var response = httpClient.SendAsync(httpRequest).Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                string messageBody = "{\"vehicleType\": \"train\",\"maxSpeed\": 125,\"avgSpeed\": 90,\"speedUnit\": \"mph in code\"}";
                var url = $"https://test01.azure-api.cn/echo/resource";
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                httpRequest.Headers.Add("Accept", "application/json, text/plain, */*");
                
                var content = new StringContent(messageBody, Encoding.UTF8, "application/json");
                httpRequest.Content = content;

                var response = httpClient.SendAsync(httpRequest).Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }



            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
