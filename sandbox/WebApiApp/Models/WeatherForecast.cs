using System.Text.Json.Serialization;
using WebApiApp.Models.Base;

namespace WebApiApp.Models;


public record WeatherForecast
{
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoInt VInt { get; set; }

    public VoDouble VDouble { get; set; }

    public VoDecimal VDecimal { get; set; }

    public VoDatetime VDatetime { get; set; }

    public VoString VString { get; set; }
}
