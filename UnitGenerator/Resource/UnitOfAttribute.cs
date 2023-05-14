﻿#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;

namespace NullableUnitGenerator;


/// <summary>
/// Attributes for automatic code generation with NullableUnitGenerator<br/>
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
/// Attribute for defining OpenApiDataType in NullableUnitGenerator<br/>
/// NullableUnitGenerator で OpenApiDataType を定義するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public partial class UnitOfOasAttribute : Attribute
{
    /// <summary>For OpenApiSchema.Type</summary>
    public string Type { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public string? Format { get; }

    /// <summary>For OpenApiSchema.Title</summary>
    public string? Title { get; }

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

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool Nullable { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public object? Example { get; }

    /// <summary>For OpenApiSchema.Description</summary>
    public string? Description { get; }

    /// <summary>
    /// https://swagger.io/docs/specification/data-models/data-types/<br/>
    /// minimum, maximum, and multipleOf are decimal values, and minLength and maxLength are int values<br/>
    /// minimum, maximum, multipleOf は decimalの値、minLength, maxLength は int型の値を指定する
    /// </summary>
    /// <param name="type">string, number, integer, boolean, ...</param>
    /// <param name="format">type:number:(-, float, double), type:integer:(-, int32, int64)</param>
    /// <param name="title">type:number:(-, float, double), type:integer:(-, int32, int64)</param>
    /// <param name="minimum">Parse to decimal</param>
    /// <param name="exclusiveMinimum">boolean</param>
    /// <param name="maximum">Parse to decimal</param>
    /// <param name="exclusiveMaximum">boolean</param>
    /// <param name="multipleOf">Parse to decimal</param>
    /// <param name="minLength">Parse to int</param>
    /// <param name="maxLength">Parse to int</param>
    /// <param name="pattern">regex pattern</param>
    /// <param name="nullable">boolean</param>
    /// <param name="example"></param>
    /// <param name="description"></param>
    /// <remarks>
    /// Types:<br/>
    /// ・string: formats (date (2017-07-21), date-time (2017-07-21T17:32:28Z), password, byte(base64-encoded characters), binary, email, uuid, uri, hostname, ipv4, ipv6, and others)<br/>
    /// ・number: <br/>
    /// ・integer<br/>
    /// ・boolean<br/>
    /// ・array<br/>
    /// ・object<br/>
    /// 
    /// 
    /// </remarks>
    public UnitOfOasAttribute(
        string type,
        string? format = null,
        string? title = null,
        object? maximum = null,
        bool exclusiveMinimum = false,
        object? minimum = null,
        bool exclusiveMaximum = false,
        object? multipleOf = null,
        object? maxLength = null,
        object? minLength = null,
        string? pattern = null,
        bool nullable = true,
        object? example = null,
        string? description = null)
    {
        Type = type;
        Format = format;
        Title = title;
        Minimum = (minimum is null) ? null : (decimal)minimum;
        ExclusiveMinimum = (minimum is null) ? null : exclusiveMinimum;
        Maximum = (maximum is null) ? null : (decimal)maximum;
        ExclusiveMaximum = (maximum is null) ? null : exclusiveMaximum;
        MultipleOf = (multipleOf is null) ? null : (decimal)multipleOf;
        MinLength = (minLength is null) ? null : (int)minLength;
        MaxLength = (maxLength is null) ? null : (int)maxLength;
        Pattern = pattern;
        Nullable = nullable;
        Example = example;
        Description = description;
    }
}
