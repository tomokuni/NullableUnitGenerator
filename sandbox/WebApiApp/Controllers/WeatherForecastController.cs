using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;
using WebApiApp.Models.Base;

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
            VInt = VoInt.NullValue, //VoInt.UndefinedValue, //VoInt = new(index)
            VDatetime = new (DateTime.Now.AddDays(index)),
            VDouble = Random.Shared.Next(-20, 55),
            VDecimal = new(Random.Shared.Next(-20, 55)),
            VString = new (Summaries[Random.Shared.Next(Summaries.Length)])
        })
        .ToArray();
    }
}
