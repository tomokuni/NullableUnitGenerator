using System.ComponentModel;
using System.Text.Json.Serialization;
using ConsoleApp.Builtins;
using ConsoleApp.Others;

namespace WebApiApp.Models;


/// <summary>ValueObjectSample</summary>
public record UnitOfSample
{
    /// <summary></summary>
    public VoString Title { get; set; }

    /// <summary></summary>
    public VoBool VoBool { get; set; }

    /// <summary></summary>
    public VoByte VoByte { get; set; }

    /// <summary></summary>
    public VoSbyte VoSbyte { get; set; }

    /// <summary></summary>
    public VoChar VoChar { get; set; }

    /// <summary></summary>
    public VoShort VoShort { get; set; }

    /// <summary></summary>
    public VoUshort VoUshort { get; set; }

    /// <summary></summary>
    public VoInt VoInt { get; set; }

    /// <summary></summary>
    public VoUint VoUint { get; set; }

    /// <summary></summary>
    public VoLong VoLong { get; set; }

    /// <summary></summary>
    public VoUlong VoUlong { get; set; }

    /// <summary></summary>
    public VoFloat VoFloat { get; set; }

    /// <summary></summary>
    public VoDouble VoDouble { get; set; }

    /// <summary></summary>
    public VoDecimal VoDecimal { get; set; }

    /// <summary></summary>
    public VoString VoString { get; set; }

    /// <summary></summary>
    public VoGuid VoGuid { get; set; }

    /// <summary></summary>
    public VoUlid VoUlid { get; set; }

    /// <summary></summary>
    public VoDatetime VoDatetime { get; set; }

    /// <summary></summary>
    public VoDateonly VoDateonly { get; set; }

    /// <summary></summary>
    public VoTimeonly VoTimeonly { get; set; }

    /// <summary></summary>
    public VoTimespan VoTimespan { get; set; }

    /// <summary></summary>
    public VoByteArray VoByteArray { get; set; }

}
