#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator でコード自動生成するための属性
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

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    public UnitOfAttribute(
        Type type,
        UnitGenerateOptions options = UnitGenerateOptions.None,
        string? toStringFormat = null)
    {
        Type = type;
        Options = options;
        ToStringFormat = toStringFormat;
    }
}


/// <summary>
/// NullableUnitGenerator で OpenApiDataType を定義する属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public partial class UnitOfOasAttribute : Attribute
{
    /// <summary>For OpenApiSchema.Type</summary>
    public string Type { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public string? Format { get; }

    /// <summary>For OpenApiSchema.Minimum</summary>
    public decimal? Minimum { get; }

    /// <summary>For OpenApiSchema.ExclusiveMinimum</summary>
    public bool? ExclusiveMinimum { get; }

    /// <summary>For OpenApiSchema.Maximum</summary>
    public decimal? Maximum { get; }

    /// <summary>For OpenApiSchema.ExclusiveMaximum</summary>
    public bool? ExclusiveMaximum { get; }

    /// <summary>For OpenApiSchema.MultipleOf</summary>
    public decimal? MultipleOf { get; }

    /// <summary>For OpenApiSchema.MinLength</summary>
    public int? MinLength { get; }

    /// <summary>For OpenApiSchema.MaxLength</summary>
    public int? MaxLength { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? Pattern { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public string? Example { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool Nullable { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">string, number, integer, boolean, ...</param>
    /// <param name="format">type:number:(-, float, double), type:integer:(-, int32, int64)</param>
    /// <param name="minimum">Parse to decimal</param>
    /// <param name="exclusiveMinimum"></param>
    /// <param name="maximum">Parse to decimal</param>
    /// <param name="exclusiveMaximum"></param>
    /// <param name="multipleOf">Parse to decimal</param>
    /// <param name="minLength">Parse to int</param>
    /// <param name="maxLength">Parse to int</param>
    /// <param name="pattern"></param>
    /// <param name="nullable"></param>
    /// <param name="example"></param>
    /// <remarks>https://swagger.io/docs/specification/data-models/data-types/</remarks>
    public UnitOfOasAttribute(
        string type,
        string? format = null,
        string? maximum = null,
        bool exclusiveMinimum = false,
        string? minimum = null,
        bool exclusiveMaximum = false,
        string? multipleOf = null,
        string? maxLength = null,
        string? minLength = null,
        string? pattern = null,
        string? example = null,
        bool nullable = true)
    {
        Type = type;
        Format = format;
        Minimum = (minimum is null) ? null : decimal.Parse(minimum);
        ExclusiveMinimum = (minimum is null) ? null : exclusiveMinimum;
        Maximum = (maximum is null) ? null : decimal.Parse(maximum);
        ExclusiveMaximum = (maximum is null) ? null : exclusiveMaximum;
        MultipleOf = (multipleOf is null) ? null : decimal.Parse(multipleOf);
        MinLength = (minLength is null) ? null : int.Parse(minLength);
        MaxLength = (maxLength is null) ? null : int.Parse(maxLength);
        Pattern = pattern;
        Nullable = nullable;
        Example = example;
    }
}
