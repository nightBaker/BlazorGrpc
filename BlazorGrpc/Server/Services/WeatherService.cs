using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather;

namespace BlazorGrpc.Server.Services
{
    public class WeatherService : WeatherForecasts.WeatherForecastsBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<GetWeatherForecastsResponse> GetWeatherForecasts(Empty request, ServerCallContext context)
        {
            var rng = new Random();
            var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow.AddDays(index).ToTimestamp(),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            var response = new GetWeatherForecastsResponse();
            response.Forecasts.AddRange(results);

            return Task.FromResult(response);
        }
    }
}
