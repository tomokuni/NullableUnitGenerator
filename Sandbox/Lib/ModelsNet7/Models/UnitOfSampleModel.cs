namespace NullableUnitGeneratorSample;


/// <summary>UnitOfSampleModel</summary>
public record UnitOfSampleModel
{
    // Builtins

    /// <summary></summary>
    public VoStringSample VoTitle { get; set; }

    /// <summary></summary>
    public VoBoolSample VoBool { get; set; }

    /// <summary></summary>
    public VoCharSample VoChar { get; set; }

    /// <summary></summary>
    public VoByteSample VoByte { get; set; }

    /// <summary></summary>
    public VoSbyteSample VoSbyte { get; set; }

    /// <summary></summary>
    public VoShortSample VoShort { get; set; }

    /// <summary></summary>
    public VoUshortSample VoUshort { get; set; }

    /// <summary></summary>
    public VoIntSample VoInt { get; set; }

    /// <summary></summary>
    public VoUintSample VoUint { get; set; }

    /// <summary></summary>
    public VoLongSample VoLong { get; set; }

    /// <summary></summary>
    public VoUlongSample VoUlong { get; set; }

    /// <summary></summary>
    public VoFloatSample VoFloat { get; set; }

    /// <summary></summary>
    public VoDoubleSample VoDouble { get; set; }

    /// <summary></summary>
    public VoDecimalSample VoDecimal { get; set; }

    /// <summary></summary>
    public VoStringSample VoString { get; set; }


    // Others

    /// <summary></summary>
    public VoGuidSample VoGuid { get; set; }

    /// <summary></summary>
    public VoUlidSample VoUlid { get; set; }

    /// <summary></summary>
    public VoDatetimeSample VoDatetime { get; set; }

    /// <summary></summary>
    public VoDateonlySample VoDateonly { get; set; }

    /// <summary></summary>
    public VoTimeonlySample VoTimeonly { get; set; }

    /// <summary></summary>
    public VoTimespanSample VoTimespan { get; set; }

    /// <summary></summary>
    public VoByteArraySample VoByteArray { get; set; }

}
