using System.Text.Json.Serialization;
using ConsoleApp.Builtins;
using ConsoleApp.Others;

namespace WebApiApp.Models;


public record WeatherForecast
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoBool VoBool { get; set; }

    public VoByte VoByte { get; set; }

    public VoSbyte VoSbyte { get; set; }

    public VoChar VoChar { get; set; }

    public VoShort VoShort { get; set; }

    public VoUshort VoUshort { get; set; }

    public VoInt VoInt { get; set; }

    public VoUint VoUint { get; set; }

    public VoNint VoNint { get; set; }

    public VoNuint VoNuint { get; set; }

    public VoLong VoLong { get; set; }

    public VoUlong VoUlong { get; set; }

    public VoFloat VoFloat { get; set; }

    public VoDouble VoDouble { get; set; }

    public VoDecimal VoDecimal { get; set; }

    public VoString VoString { get; set; }

    public VoUrlSafeBinary VoUrlSafeBinary { get; set; }

    public VoGuid VoGuid { get; set; }

    public VoUlid VoUlid { get; set; }

    public VoDatetime VoDatetime { get; set; }

    public VoDateonly VoDateonly { get; set; }

    public VoTimeonly VoTimeonly { get; set; }

    public VoTimespan VoTimespan { get; set; }

    public VoByteArray VoByteArray { get; set; }
}
