#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.Text;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGenerator の OpenApiDataType を定義する属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
internal class UnitOfOpenApiDataTypeAttribute : Attribute
{
    /// <summary>For OpenApiSchema.Tepe</summary>
    public string Type { get; }

    /// <summary>For OpenApiSchema.Format</summary>
    public string? Format { get; }

    /// <summary>For OpenApiSchema.Example</summary>
    public object? Example { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? Pattern { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool Nullable { get; } = true;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="format"></param>
    /// <param name="example"></param>
    /// <param name="pattern"></param>
    public UnitOfOpenApiDataTypeAttribute(
        string type,
        string? format = null,
        object? example = null,
        string? pattern = null)
    {
        Type = type;
        Format = format;
        Example = example;
        Pattern = pattern;
    }
}
