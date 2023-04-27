#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.Text;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator でコード自動生成するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfAttribute : Attribute
{
    /// <summary>primitive type</summary>
    public Type Type { get; }

    /// <summary>UnitGenerateOptions</summary>
    public UnitGenerateOptions Options { get; }

    /// <summary>ToStringFormat</summary>
    public string? ToStringFormat { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    public UnitOfAttribute(Type type, UnitGenerateOptions options = UnitGenerateOptions.None, string? toStringFormat = null)
    {
        Type = type;
        Options = options;
        ToStringFormat = toStringFormat;
    }
}


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
