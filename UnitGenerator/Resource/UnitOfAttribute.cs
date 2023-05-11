#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Diagnostics.CodeAnalysis;

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

    /// <summary>For OpenApiSchema.Format</summary>
    public decimal? Maximum { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public decimal? Minimum { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public int? MaxLength { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public int? MinLength { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? Pattern { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool Nullable { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public string? Example { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="format"></param>
    /// <param name="maximum"></param>
    /// <param name="minimum"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <param name="pattern"></param>
    /// <param name="nullable"></param>
    /// <param name="example"></param>
    public UnitOfOasAttribute(
        [DisallowNull] string type,
        [AllowNull] string? format = null,
        [AllowNull] decimal? maximum = null,
        [AllowNull] decimal? minimum = null,
        [AllowNull] int? maxLength = null,
        [AllowNull] int? minLength = null,
        [AllowNull] string? pattern = null,
        [AllowNull] string? example = null,
        bool nullable = true)
    {
        Type = type;
        Format = format;
        Maximum = maximum;
        Minimum = minimum;
        MaxLength = maxLength;
        MinLength = minLength;
        Pattern = pattern;
        Nullable = nullable;
        Example = example;
    }
}
