using System;

namespace NullableUnitGenerator;


/// <summary>
/// UnitGenerate の生成オプション
/// </summary>
[Flags]
public enum UnitGenerateOptions
{
    /// <summary>None</summary>
    None = 0,

    /// <summary>ImplicitOperator</summary>
    ImplicitOperator = 1,

    /// <summary>ParseMethod</summary>
    ParseMethod = 2,

    /// <summary>MinMaxMethod</summary>
    MinMaxMethod = 4,

    /// <summary>ArithmeticOperator</summary>
    ArithmeticOperator = 8,

    /// <summary>ValueArithmeticOperator</summary>
    ValueArithmeticOperator = 16,

    /// <summary>Comparable</summary>
    Comparable = 32,

    /// <summary>Validate</summary>
    Validate = 64,

    /// <summary>JsonConverter</summary>
    JsonConverter = 128,

    /// <summary>MessagePackFormatter</summary>
    MessagePackFormatter = 256,

    /// <summary>DapperTypeHandler</summary>
    DapperTypeHandler = 512,

    /// <summary>EntityFrameworkValueConverter</summary>
    EntityFrameworkValueConverter = 1024,

    /// <summary>WithoutComparisonOperator</summary>
    WithoutComparisonOperator = 2048,

    /// <summary>JsonConverterDictionaryKeySupport</summary>
    JsonConverterDictionaryKeySupport = 4096,

    /// <summary>StandardPrimitive</summary>
    IntegralFull = ImplicitOperator | ParseMethod | MinMaxMethod | ArithmeticOperator | ValueArithmeticOperator | Comparable | JsonConverter | JsonConverterDictionaryKeySupport,
}
