using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;

namespace WebApi.Controllers;
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

    [HttpPost(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get(WeatherForecast wf)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            VoInt = new(index),
            VoDatetime = new (DateTime.Now.AddDays(index)),
            VoDouble = new (Random.Shared.Next(-20, 55)),
            //VoDecimal = new(Random.Shared.Next(-20, 55)),
            VoString = new (Summaries[Random.Shared.Next(Summaries.Length)])
        })
        .ToArray();
    }
}
