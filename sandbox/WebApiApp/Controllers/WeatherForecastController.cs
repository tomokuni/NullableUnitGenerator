using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;
using ConsoleApp.Builtins;
using ConsoleApp.Others;

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
        return Enumerable.Range(1, 1).Select(index => new WeatherForecast
        {
            VoInt = VoInt.NullValue, //VoInt.UndefinedValue, //VoInt = new(index)
            VoDatetime = new (DateTime.Now.AddDays(index)),
            VoDouble = Random.Shared.Next(-20, 55),
            VoDecimal = Random.Shared.Next(-20, 55),
            VoString = new (Summaries[Random.Shared.Next(Summaries.Length)])
        })
        .ToArray();
    }
}
