﻿#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator でコード自動生成するための属性、OpenApiDataType を定義する属性
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

    /// <summary>For OpenApiSchema.Format</summary>
    public string? OpenApiFormat { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public object? OpenApiExample { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? OpenApiPattern { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool? OpenApiNullable { get; } = true;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    /// <param name="openApiformat"></param>
    /// <param name="openApiexample"></param>
    /// <param name="openApipattern"></param>
    public UnitOfAttribute(
        Type type,
        UnitGenerateOptions options = UnitGenerateOptions.None,
        string? toStringFormat = null,
        string? openApiformat = null,
        object? openApiexample = null,
        string? openApipattern = null)
    {
        Type = type;
        Options = options;
        ToStringFormat = toStringFormat;
        OpenApiFormat = openApiformat;
        OpenApiExample = openApiexample;
        OpenApiPattern = openApipattern;
    }
}
