#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator でコード自動生成するための属性 と OpenApiDataType を定義する属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public partial class UnitOfAttribute : Attribute
{
    /// <summary>primitive type</summary>
    public Type Type { get; }

    /// <summary>UnitGenerateOptions</summary>
    public UnitGenerateOptions Options { get; }

    /// <summary>ToStringFormat</summary>
    public string? ToStringFormat { get; }

    /// <summary>For OpenApiSchema.Type</summary>
    public string? OasType { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public string? OasFormat { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? OasPattern { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool OasNullable { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public string? OasExample { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    /// <param name="oasType"></param>
    /// <param name="oasFormat"></param>
    /// <param name="oasPattern"></param>
    /// <param name="oasNullable"></param>
    /// <param name="oasExample"></param>
    public UnitOfAttribute(
        Type type,
        UnitGenerateOptions options = UnitGenerateOptions.None,
        string? toStringFormat = null,
        string? oasType = null,
        string? oasFormat = null,
        string? oasPattern = null,
        bool oasNullable = true,
        string? oasExample = null)
    {
        Type = type;
        Options = options;
        ToStringFormat = toStringFormat;
        OasType = oasType;
        OasFormat = oasFormat;
        OasPattern = oasPattern;
        OasNullable = oasNullable;
        OasExample = oasExample;
    }
}
