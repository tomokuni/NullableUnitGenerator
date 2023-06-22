﻿#pragma warning disable IDE0079 // 不要な抑制を削除します
#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NullableUnitGenerator;


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
    public UnitGenOpts Options { get; }

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
        UnitGenOpts options = UnitGenOpts.None,
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
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    public UnitOfAttribute(
        UnitGenOpts options = UnitGenOpts.None,
        string? toStringFormat = null)
        : base(typeof(T), 
               options: options, 
               toStringFormat: toStringFormat)
    {}
}
#endif


/// <summary>
/// Attribute for defining OpenApiDataType in NullableUnitGenerator<br/>
/// NullableUnitGenerator で OpenApiDataType を定義するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasAttribute : Attribute
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
    /// <param name="type">string, number, integer, boolean, array, object</param>
    /// <param name="format">type:string:(-, date, time, date-time, password, byte, email, uuid, uri, hostname, ipv4, ipv6), type:number:(-, float, double), type:integer:(-, int32, int64)</param>
    /// <param name="title"></param>
    /// <param name="minimum">Parse to decimal</param>
    /// <param name="exclusiveMinimum">boolean</param>
    /// <param name="maximum">Parse to decimal</param>
    /// <param name="exclusiveMaximum">boolean</param>
    /// <param name="multipleOf">Parse to decimal</param>
    /// <param name="minLength">Parse to int</param>
    /// <param name="maxLength">Parse to int</param>
    /// <param name="pattern">regex pattern</param>
    /// <param name="nullable">boolean</param>
    /// <param name="example">example</param>
    /// <param name="description">description</param>
    /// <remarks>
    /// https://swagger.io/docs/specification/data-models/data-types/<br/>
    /// Types:<br/>
    /// ・string:  format : (-, date(2017-07-21), time(17:32:28), date-time(2017-07-21T17:32:28Z), password, byte(base64-encoded characters), binary, email, uuid, uri, hostname, ipv4, ipv6)<br/>
    /// ・number:  format : (-, float, double)<br/>
    /// ・integer: format : (-, int32, int64)<br/>
    /// ・boolean<br/>
    /// ・array<br/>
    /// ・object<br/>
    /// </remarks>
    public UnitOfOasAttribute(
        string type,
        string? format = null,
        string? title = null,
        object? minimum = null,
        bool exclusiveMaximum = false,
        object? maximum = null,
        bool exclusiveMinimum = false,
        object? multipleOf = null,
        object? minLength = null,
        object? maxLength = null,
        string? pattern = null,
        bool nullable = true,
        object? example = null,
        string? description = null)
    {
        Type = type;
        Format = format;
        Title = title;
        Minimum = (minimum is null) ? null : (decimal)minimum;
        ExclusiveMinimum = exclusiveMinimum;
        Maximum = (maximum is null) ? null : (decimal)maximum;
        ExclusiveMaximum = exclusiveMaximum;
        MultipleOf = (multipleOf is null) ? null : (decimal)multipleOf;
        MinLength = (minLength is null) ? null : (int)minLength;
        MaxLength = (maxLength is null) ? null : (int)maxLength;
        Pattern = pattern;
        Nullable = nullable;
        Example = example;
        Description = description;
    }
}




/// <summary>
/// Validation attribute to assert Range. 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfRangeAttribute : RangeAttribute, IUnitValidationAttribute
{
    /// <summary>Constructor</summary>
    /// <param name="minimum">The minimum value, inclusive</param>
    /// <param name="maximum">The maximum value, inclusive</param>
    public UnitOfRangeAttribute(int minimum, int maximum) : base(minimum, maximum) { }

    /// <summary>Constructor</summary>
    /// <param name="minimum">The minimum value, inclusive</param>
    /// <param name="maximum">The maximum value, inclusive</param>
    public UnitOfRangeAttribute(double minimum, double maximum) : base(minimum, maximum) { }

    /// <summary>Constructor</summary>
    /// <param name="type">The type of the range parameters. Must implement IComparable.</param>
    /// <param name="minimum">The minimum allowable value.</param>
    /// <param name="maximum">The maximum allowable value.</param>
    public UnitOfRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum) { }

    public override bool IsValid(object? value)
        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetRawValueAsObject()));
}


/// <summary>
/// Validation attribute to assert StringLength. 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfStringLengthAttribute : StringLengthAttribute, IUnitValidationAttribute
{
    /// <summary>Constructor</summary>
    /// <param name="maximumLength"></param>
    public UnitOfStringLengthAttribute(int maximumLength) : base(maximumLength) { }

    public override bool IsValid(object? value)
        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetRawValueAsObject()));
}


/// <summary>
/// Validate based on attribute of UnitOf definition.<br/>
/// UnitOf 定義の属性に基づき検証を行する。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfDefinedValidateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        // Validationを実施
        var context = new ValidationContext(value, null, null);
        var validationResults = value.GetType().GetCustomAttributes<ValidationAttribute>()
            .Where(s => s is IUnitValidationAttribute)
            .Select(s => s.GetValidationResult(this, context)!)
            .Where(s => s != ValidationResult.Success);

        if (!validationResults.Any())
            return ValidationResult.Success;

        var result = validationResults
            .Select(s => new ValidationResult(s.ErrorMessage, s.MemberNames.Select(y => $"{validationContext.DisplayName}.{y}")));
        var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", new { validationContext.DisplayName }));
        compositeResults.AddResults(result);
        return compositeResults;
    }
}


/// <summary>
/// Validate based on attributes of the child object property.<br/>
/// 子オブジェクトのプロパティの属性に基づき検証する。
/// </summary>
public class NestedValidateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) 
            return ValidationResult.Success;
        
        var results = new List<ValidationResult>();
        var context = new ValidationContext(value, null, null);
        // Validationを実施
        Validator.TryValidateObject(value, context, results, true);

        if (results.Count == 0)
            return ValidationResult.Success;

        var validationResults = results.Select(x => new ValidationResult(x.ErrorMessage, x.MemberNames.Select(y => $"{validationContext.DisplayName}.{y}")));
        var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", new { validationContext.DisplayName }));
        compositeResults.AddResults(validationResults);
        return compositeResults;
    }
}


/// <summary>
/// 複数のValidation結果を格納できる <see cref="ValidationResult" />
/// </summary>
public class CompositeValidationResult : ValidationResult
{
    /// <summary>
    /// 複数のValidation結果
    /// </summary>
    public IEnumerable<ValidationResult> Results => _result.AsReadOnly();
    private List<ValidationResult> _result = new List<ValidationResult>();

    public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
    public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
    protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }
    
    /// <summary>
    /// エラーを登録
    /// </summary>
    /// <param name="validationResult"></param>
    public void AddResult(ValidationResult validationResult)
        => _result.Add(validationResult);

    /// <summary>
    /// エラーを登録
    /// </summary>
    /// <param name="validationResults"></param>
    public void AddResults(IEnumerable<ValidationResult> validationResults)
        => _result.AddRange(validationResults);
}