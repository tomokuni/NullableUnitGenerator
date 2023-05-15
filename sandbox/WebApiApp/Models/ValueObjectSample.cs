using System.ComponentModel;
using System.Text.Json.Serialization;
using ConsoleApp.Builtins;
using ConsoleApp.Others;

namespace WebApiApp.Models;


/// <summary>ValueObjectSample</summary>
public record ValueObjectSample
{
    /// <summary></summary>
    public VoString Title { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoBool VoBool { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoByte VoByte { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoSbyte VoSbyte { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoChar VoChar { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoShort VoShort { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoUshort VoUshort { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoInt VoInt { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoUint VoUint { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoLong VoLong { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoUlong VoUlong { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoFloat VoFloat { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoDouble VoDouble { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoDecimal VoDecimal { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoString VoString { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoUrlSafeBinary VoUrlSafeBinary { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoGuid VoGuid { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoUlid VoUlid { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoDatetime VoDatetime { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoDateonly VoDateonly { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoTimeonly VoTimeonly { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoTimespan VoTimespan { get; set; }

    /// <summary></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public VoByteArray VoByteArray { get; set; }
}
