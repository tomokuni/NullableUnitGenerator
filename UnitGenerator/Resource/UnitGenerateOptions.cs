
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

    /// <summary>PrivitiveFull</summary>
    /// <remarks>ArithmeticOperator | ValueArithmeticOperator | ComparisonOperator | IComparable | ImplicitOperator | ParseMethod | MinMaxMethod</remarks>
    PrivitiveFull = ArithmeticOperator | ValueArithmeticOperator | ComparisonOperator | IComparable | ImplicitOperator | ParseMethod | MinMaxMethod,

    /// <summary>ArithmeticOperator</summary>
    ArithmeticOperator = 8,

    /// <summary>ValueArithmeticOperator</summary>
    ValueArithmeticOperator = 16,

    /// <summary>ComparisonOperator</summary>
    ComparisonOperator = 2048,

    /// <summary>IComparable</summary>
    IComparable = 32,

    /// <summary>ImplicitOperator</summary>
    ImplicitOperator = 1,

    /// <summary>ParseMethod</summary>
    ParseMethod = 2,

    /// <summary>MinMaxMethod</summary>
    MinMaxMethod = 4,

    /// <summary>Validate</summary>
    Validate = 64,

    /// <summary>JsonConverter</summary>
    JsonConverter = 128,

    /// <summary>JsonConverterDictionaryKeySupport</summary>
    JsonConverterDictionaryKeySupport = 4096,

    /// <summary>DapperTypeHandler</summary>
    DapperTypeHandler = 512,

    /// <summary>EntityFrameworkValueConverter</summary>
    EntityFrameworkValueConverter = 1024,

    /// <summary>MessagePackFormatter</summary>
    MessagePackFormatter = 256,
}
