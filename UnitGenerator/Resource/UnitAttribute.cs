#pragma warning disable IDE0079 // 不要な抑制を削除します
#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

    /// <summary>For conversion to and from Json strings</summary>
    public string? ParseString { get; }

    /// <summary>
    /// Attributes for OpenApiDataType definitions or constraints<br/>
    /// OpenApiDataType 定義 または 制約用の属性
    /// </summary>
    /// <param name="type">Type Format set by OpenApiDataType. <br/>date, time, date-time, phone, email, uri as string and have format constraint. <br/><br/>string, number, integer, boolean, date, time, date-time, phone, email, uri</param>
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
        string? description = null,
        string? parseString = null)
    {
        Title = title;
        Pattern = regex;
        Example = example;
        Description = description;
        Nullable = true;
        ParseString = parseString;

        // type, format
        (Type, Format) = type switch
        {
            "string" => ("string", null),
            "number" => ("number", null),
            "integer" => ("integer", null),
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
            throw new ArgumentException($"renge {range} is invalid value.");

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
            throw new ArgumentException($"length {range} is invalid value.");
    }

}


/// <summary>
/// Validate based on UnitOfOasAttribute of UnitOf definition.<br/>
/// UnitOf定義のUnitOfOas属性に基づいて検証する。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfValidateBaseOnUnitOfOasAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // 検証不要なら、Success
        if (value is null)
            return ValidationResult.Success!;
        if (value is not IUnitOf u)
            return ValidationResult.Success!;
        if (!u.HasValue)
            return ValidationResult.Success!;

        var type = value.GetType();
        var attr = value.GetType().GetCustomAttributes<UnitOfOasAttribute>().SingleOrDefault();
        if (attr is null)
            return ValidationResult.Success;

        // Validationを実施
        ValidationResult msg;
        var val = u.GetRawValueAsObject();
        switch (attr.Type)
        {
            case "string":
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "number":
                msg = ValidateRange(val, typeof(decimal), attr.Minimum, attr.Maximum);
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                break;
            case "integer":
                msg = ValidateRange(val, typeof(long), attr.Minimum, attr.Maximum);
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                break;
            case "boolean":
                break;
            case "date":
                msg = ValidateRange(val, typeof(DateOnly), attr.Minimum, attr.Maximum);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "time":
                msg = ValidateRange(val, typeof(TimeOnly), attr.Minimum, attr.Maximum);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "date-time":
                msg = ValidateRange(val, typeof(DateTime), attr.Minimum, attr.Maximum);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "phone":
                msg = ValidatePhoneNumber(val);
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "email":
                msg = ValidateEmailAddress(val);
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
            case "uri":
                msg = ValidateUrl(val);
                msg = ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength);
                msg = ValidateRegularExpression(val, attr.Pattern);
                break;
        }
        return ValidationResult.Success;

        //if (!validationResults.Any())
        //    return ValidationResult.Success;

        //var result = validationResults
        //    .Select(s => new ValidationResult(s.ErrorMessage, s.MemberNames.Select(y => $"{validationContext.DisplayName}.{y}")));
        //var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", new { validationContext.DisplayName }));
        //compositeResults.AddResults(result);
        //return compositeResults;
    }

    private static ValidationResult ValidateLength(object? val, int? minLen, int? maxLen)
    {
        if (val is null || maxLen is null || (minLen is null && maxLen is null))
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        return (minLen, maxLen) switch
        {
            (not null, not null)
                => new StringLengthAttribute(maxLen.Value) { MinimumLength = minLen.Value }.GetValidationResult(val, ctx)!,
            (null, not null)
                => new StringLengthAttribute(maxLen.Value).GetValidationResult(val, ctx)!,
            _ => ValidationResult.Success!,
        };
    }

    private static ValidationResult ValidateRange(object? val, Type type, string? min, string? max)
    {
        if (val is null || string.IsNullOrWhiteSpace(min) || string.IsNullOrWhiteSpace(max))
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        return new RangeAttribute(type, min, max).GetValidationResult(val, ctx)!;
    }

    private static ValidationResult ValidateRegularExpression(object? val, string? pattern)
    {
        if (val is null || string.IsNullOrWhiteSpace(pattern))
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        var res = new RegularExpressionAttribute(pattern).GetValidationResult(val, ctx)!;
        res.ErrorMessage = res.ErrorMessage;
        return res;
    }

    private static ValidationResult ValidatePhoneNumber(object? val)
    {
        if (val is null)
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        return new PhoneAttribute().GetValidationResult(val, ctx)!;
    }

    private static ValidationResult ValidateEmailAddress(object? val)
    {
        if (val is null)
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        return new EmailAddressAttribute().GetValidationResult(val, ctx)!;
    }

    private static ValidationResult ValidateUrl(object? val)
    {
        if (val is null)
            return ValidationResult.Success!;

        var ctx = new ValidationContext(val);
        return new UrlAttribute().GetValidationResult(val, ctx)!;
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


///// <summary>
///// Validate based on attribute of UnitOf definition.<br/>
///// UnitOf 定義の属性に基づき検証を行する。
///// </summary>
//[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, AllowMultiple = false)]
//public class UnitOfDefinedValidateAttribute : ValidationAttribute
//{
//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        if (value == null)
//            return ValidationResult.Success;
//
//        // Validationを実施
//        var context = new ValidationContext(value, null, null);
//        var validationResults = value.GetType().GetCustomAttributes<ValidationAttribute>()
//            .Where(s => s is IUnitValidationAttribute)
//            .Select(s => s.GetValidationResult(this, context)!)
//            .Where(s => s != ValidationResult.Success);
//
//        if (!validationResults.Any())
//            return ValidationResult.Success;
//
//        var result = validationResults
//            .Select(s => new ValidationResult(s.ErrorMessage, s.MemberNames.Select(y => $"{validationContext.DisplayName}.{y}")));
//        var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", new { validationContext.DisplayName }));
//        compositeResults.AddResults(result);
//        return compositeResults;
//    }
//}


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