using System.Text.Json.Serialization;
using WebApi.Model.Base;

namespace WebApiApp.Models;


public record WeatherForecast
{
    public VoInt VoInt { get; set; }

    public VoDouble VoDouble { get; set; }

    public VoDecimal VoDecimal { get; set; }

    public VoDatetime VoDatetime { get; set; }

    public VoString VoString { get; set; }
}
