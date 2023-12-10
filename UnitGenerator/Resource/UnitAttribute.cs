using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NullableUnitGenerator;

#pragma warning disable IDE0079 // 不要な抑制を削除します
#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。


/// <summary>
/// Attributes for automatic code generation with NullableUnitGenerator<br/>
/// NullableUnitGenerator でコード自動生成するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfAttribute : Attribute
{
    /// <summary>primitive type</summary>
    public Type Type { get; }

    /// <summary>UnitGenerater Options</summary>
    public UnitGenerateOption Options { get; }

    /// <summary>ToStringFormat</summary>
    public string? ToStringFormat { get; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type">primitive type</param>
    /// <param name="options">UnitGenerater Options</param>
    /// <param name="toStringFormat">ToStringFormat</param>
    public UnitOfAttribute(
        Type type,
        UnitGenerateOption options = UnitGenerateOption.None,
        string? toStringFormat = null)
    {
        Type = type;
        Options = options;
        ToStringFormat = toStringFormat;
    }
}

#if NET7_0_OR_GREATER
/// <summary>
/// Attributes for automatic code generation with NullableUnitGenerator<br/>
/// NullableUnitGenerator でコード自動生成するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public partial class UnitOfAttribute<T> : UnitOfAttribute
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">UnitGenerater Options</param>
    /// <param name="toStringFormat">ToStringFormat</param>
    public UnitOfAttribute(
        UnitGenerateOption options = UnitGenerateOption.None,
        string? toStringFormat = null)
        : base(typeof(T), 
               options: options, 
               toStringFormat: toStringFormat)
    {}
}
#endif
