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
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
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
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    /// <param name="toStringFormat"></param>
    public UnitOfAttribute(
        UnitGenerateOption options = UnitGenerateOption.None,
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
    public string? Minimum { get; }

    /// <summary>For OpenApiSchema.Maximum</summary>
    public string? Maximum { get; }

    /// <summary>For OpenApiSchema.MinLength</summary>
    public int? MinLength { get; }

    /// <summary>For OpenApiSchema.MaxLength</summary>
    public int? MaxLength { get; }

    /// <summary>For OpenApiSchema.Pattern</summary>
    public string? Pattern { get; }
    
    /// <summary>For OpenApiSchema.Example</summary>
    public object? Example { get; }

    /// <summary>For OpenApiSchema.Description</summary>
    public string? Description { get; }

    /// <summary>For OpenApiSchema.Nullable</summary>
    public bool Nullable { get; }

    /// <summary>
    /// Attributes for OpenApiDataType definitions or constraints<br/>
    /// OpenApiDataType 定義 または 制約用の属性
    /// </summary>
    /// <param name="type">Type Format set by OpenApiDataType. <br/>date, time, date-time, phone, email, uri as string and have format constraint. <br/><br/>string, integer, number, boolean, date, time, date-time, phone, email, uri</param>
    /// <param name="title">Title set by OpenApiDataType.</param>
    /// <param name="range">Minimam and Maximam set by OpenApiDataType. <br/><br/>Parse to minimum and maximum values as decimal type.<br/>For number and integer.<br/><b>format : </b>min-max<br/><b>example : </b>11-222</param>
    /// <param name="length">MinLength and MaxLength set by OpenApiDataType .<br/><br/>Parse to minimum and maximum length as int type.<br/>For string.<br/><b>format : </b>min-max or fix<br/><b>example : </b>1-2 or 3</param>
    /// <param name="regex">Pattern set by OpenApiDataType. <br/><br/>For string.</param>
    /// <param name="example">example set by OpenApiDataType.</param>
    /// <param name="description">description set by OpenApiDataType.</param>
    /// <param name="parseString">For conversion to and from Json strings.</param>
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
        string? title = null,
        string? range = null,
        string? length = null,
        string? regex = null,
        object? example = null,
        string? description = null)
    {
        Title = title;
        Pattern = regex;
        Example = example;
        Description = description;
        Nullable = true;

        // type, format
        (Type, Format) = type switch
        {
            "string" => ("string", null),
            "integer" => ("integer", null),
            "number" => ("number", null),
            "boolean" => ("boolean", null),
            "date" => ("string", "date"),
            "time" => ("string", "time"),
            "date-time" => ("string", "date-time"),
            "phone" => ("string", "phone"),
            "email" => ("string", "email"),
            "uri" => ("string", "uri"),
            _ => ("string", null)
        };

        // range
        if (range is null)
        {
            Minimum = null;
            Maximum = null;
        }
        else if (Regex.IsMatch(range, @"^\d+-\d+$"))
        {
            var splitRange = range.Split('-');
            Minimum = splitRange[0];
            Maximum = splitRange[1];
        }
        else
            throw new ArgumentException($"renge {range} is invalid value. format is [^\\d+-\\d+$]");

        // length
        if (length is null)
        {
            MinLength = null;
            MaxLength = null;
        }
        else if (Regex.IsMatch(length, @"^\d+$"))
        {
            MinLength = null;
            MaxLength = int.Parse(length);
        }
        else if (Regex.IsMatch(length, @"^\d+-\d+$"))
        {
            var splitLength = length.Split('-')!;
            MinLength = int.Parse(splitLength[0]);
            MaxLength = int.Parse(splitLength[1]);
        }
        else
            throw new ArgumentException($"length {range} is invalid value. format is [^\\d+$|^\\d+-\\d+$]");
    }

}


/// <summary>
/// Validate based on UnitOfOasAttribute of UnitOf definition.<br/>
/// UnitOf定義のUnitOfOas属性に基づいて検証する。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfOasValidateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        // 検証不要なら、Success
        if (value is not IValidatableObject v)
            return ValidationResult.Success!;

        // Validationを実施
        var results = v.Validate(ctx);
        if (!results.Any())
            return ValidationResult.Success;
        
        var compositeResults = new CompositeValidationResult(ErrorMessage, results.First().MemberNames);
        //var compositeResults = new CompositeValidationResult($"Validation for {ctx.DisplayName} failed!", results.First().MemberNames);
        compositeResults.AddResults(results);
        return compositeResults;
    }
}




// <summary>
/// Validation attribute to assert Range. 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
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
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfStringLengthAttribute : StringLengthAttribute, IUnitValidationAttribute
{
    /// <summary>Constructor</summary>
    /// <param name="maximumLength"></param>
    public UnitOfStringLengthAttribute(int maximumLength) : base(maximumLength) { }

    public override bool IsValid(object? value)
        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetRawValueAsObject()));
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
