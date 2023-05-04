
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

    /// <summary>MaxExtent</summary>
    /// <remarks>ImplicitOperator | IComparable | ComparisonOperator | ArithmeticOperator | ValueArithmeticOperator | ParseMethod | MinMaxMethod</remarks>
    MaxExtent = ImplicitOperator | IComparable | ComparisonOperator | ArithmeticOperator | ValueArithmeticOperator | ParseMethod | MinMaxMethod,

    /// <summary>ImplicitOperator</summary>
    ImplicitOperator = 0b0000_0000_0000_0001,

    /// <summary>IComparable</summary>
    IComparable = 0b0000_0000_0000_0010,

    /// <summary>ComparisonOperator</summary>
    ComparisonOperator = 0b0000_0000_0000_0100,

    /// <summary>ArithmeticOperator</summary>
    ArithmeticOperator = 0b0000_0000_0000_1000,

    /// <summary>ValueArithmeticOperator</summary>
    ValueArithmeticOperator = 0b0000_0000_0001_0000,

    /// <summary>ParseMethod</summary>
    ParseMethod = 0b0000_0000_0010_0000,

    /// <summary>MinMaxMethod</summary>
    MinMaxMethod = 0b0000_0000_0100_0000,

    /// <summary>Validate</summary>
    Validate = 0b0001_0000_0000_0000,

    /// <summary>JsonConverterSupport</summary>
    JsonConverterSupport = 0b0000_0000_0001_0000_0000_0000_0000_0000,

    /// <summary>JsonConverterDictionaryKeySupport</summary>
    JsonConverterDictionaryKeySupport = 0b0000_0000_0010_0000_0000_0000_0000_0000,

    /// <summary>MessagePackFormatterSupport</summary>
    MessagePackFormatterSupport = 0b0000_0000_1000_0000_0000_0000_0000_0000,

    /// <summary>DapperTypeHandlerSupport</summary>
    DapperTypeHandlerSupport = 0b0000_0001_0000_0000_0000_0000_0000_0000,

    /// <summary>EntityFrameworkValueConverterSupport</summary>
    EntityFrameworkValueConverterSupport = 0b0000_0010_0000_0000_0000_0000_0000_0000,
}
