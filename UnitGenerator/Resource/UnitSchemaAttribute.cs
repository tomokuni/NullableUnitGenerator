using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using NullableUnitGenerator.ExtensionMethods;

namespace NullableUnitGenerator;



/// <summary>
/// Attribute for defining OpenApiDataType in NullableUnitGenerator<br/>
/// NullableUnitGenerator で OpenApiDataType を定義するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfSchemaAttribute : Attribute
{
    /// <summary>for OpenApiSchema.Type</summary>
    public string? Type { get => _type; init => _type = value?.EmptyToNull(); }
    private string? _type = null;

    /// <summary>for OpenApiSchema.Format</summary>
    public string? Format { get => _format; init => _format = value.EmptyToNull(); }
    private string? _format = null;

    /// <summary>for OpenApiSchema.Nullable</summary>
    public bool Nullable { get; init; } = true;

    /// <summary>for OpenApiSchema.Title</summary>
    public string? Title { get => _title; init => _title = value.EmptyToNull(); }
    private string? _title = null;

    /// <summary>for OpenApiSchema.Summary</summary>
    public string? Summary { get => _summary; init => _summary = value.EmptyToNull(); }
    private string? _summary = null;

    /// <summary>For OpenApiSchema.Description</summary>
    public string? Description { get => _description; init => _description = value.EmptyToNull(); }
    private string? _description = null;

    /// <summary>for OpenApiSchema.Example</summary>
    public object? Example { get; init; }

    /// <summary>for OpenApiSchema.Maximum</summary>
    public object? Maximum
    {
        get => _maximum;
        set => _maximum = value ?? throw Exception();
    }
    public decimal? _maximum = null;

    /// <summary>for OpenApiSchema.Maximum (as datetime)</summary>
    public DateTime? MaximumDateTime { get => Maximum?.ToDateTime(); init => Maximum = value?.ToUnixTimeSeconds(); }

    /// <summary>for OpenApiSchema.ExclusiveMaximum</summary>
    public bool? ExclusiveMaximum { get; init; }

    /// <summary>for OpenApiSchema.Minimum</summary>
    public object? Minimum
    {
        get => _minimum;
        set => _minimum = value;
    }
    public decimal? _minimum = null;

    /// <summary>for OpenApiSchema.Minimum (as datetime)</summary>
    public DateTime? MinimumDateTime { get => Minimum?.ToDateTime(); init => Minimum = value?.ToUnixTimeSeconds(); }

    /// <summary>for OpenApiSchema.ExclusiveMinimum</summary>
    public bool? ExclusiveMinimum { get; init; }

    /// <summary>for OpenApiSchema.MaxLength</summary>
    public int? MaxLength { get; init; }

    /// <summary>for OpenApiSchema.MinLength</summary>
    public int? MinLength { get; init; }

    /// <summary>for OpenApiSchema.Pattern</summary>
    public string? Pattern { get => _pattern; init => _pattern = value.EmptyToNull(); }
    private string? _pattern = null;


    /// <summary>Pattern for basic format of date.<br/> (yyyyMMdd)</summary>
    public const string PatternDateBasic = @"\d{8}";
    /// <summary>Pattern for extended format of date.<br/> (YYYY-MM-DD)</summary>
    public const string PatternDateExtended = @"\d{4}-[01]\d-[03]\d";
    /// <summary>Pattern for lax format of date.</summary>
    public const string PatternDateLax = @"\d{4}[\-\./][01]?\d[\-\./][03]?\d";
    /// <summary>Pattern of date.<br/> (basic or extended or lax format.)</summary>
    public const string PatternDate = @$"{PatternDateBasic}|{PatternDateExtended}|{PatternDateLax}";

    /// <summary>Pattern for basic format of time without T.<br/> (hhmmss[.sss][Z|+hhmm])</summary>
    public const string PatternTimeBasicWithoutT = @"\d{6}(\.\d+)?(Z|z|[\+-]\d{4})?";
    /// <summary>Pattern for basic format of time.<br/> (Thhmmss[.sss][Z|+hhmm])</summary>
    public const string PatternTimeBasic = @$"[Tt]?{PatternTimeBasicWithoutT}";
    /// <summary>Pattern for extended format of time without T.<br/> (hh:mm:ss[.sss][Z|+hh:mm])</summary>
    public const string PatternTimeExtendedWithoutT = @"\d{2}:\d{2}:\d{2}(\.\d+)?(Z|z|[\+-]\d{2}:d{2})?";
    /// <summary>Pattern for extended format of time.<br/> (Thh:mm:ss[.sss][Z|+hh:mm])</summary>
    public const string PatternTimeExtended = @$"[Tt]?{PatternTimeExtendedWithoutT}";
    /// <summary>Pattern for lax format of time without T.</summary>
    public const string PatternTimeLaxWithoutT = @"\d?\d:\d?\d:\d?\d(\.\d+)?(Z|z|[\+-]\d?\d:\d?\d)?";
    /// <summary>Pattern for lax format of time.</summary>
    public const string PatternTimeLax = @$"[Tt]?{PatternTimeLaxWithoutT}";
    /// <summary>Pattern for time without T.<br/> (basic or extended or lax format.)</summary>
    public const string PatternTimeWithoutT = @$"{PatternTimeBasicWithoutT}|{PatternTimeExtendedWithoutT}|{PatternTimeLaxWithoutT}";
    /// <summary>Pattern of time.<br/> (basic or extended or lax format.)</summary>
    public const string PatternTime = @$"[Tt]?{PatternTimeWithoutT}";

    /// <summary>Pattern for basic format of datetime.<br/> (yyyyMMddThhmmss[.sss][Z|+hhmm])</summary>
    public const string PatternDateTimeBasic = @$"{PatternDateBasic}{PatternTimeBasic}";
    /// <summary>Pattern for extended format of datetime.<br/> ((YYYY-MM-DDThh:mm:ss[.sss][Z|+hh:mm])</summary>
    public const string PatternDateTimeExtended = @$"{PatternDateExtended}{PatternTimeExtended}";
    /// <summary>Pattern for lax format of datetime.</summary>
    public const string PatternDateTimeLax = @$"{PatternDateLax}[Tt ]{PatternTimeLaxWithoutT}";  // lax format.
    /// <summary>Pattern of datetime.<br/> (basic or extended or lax format.)</summary>
    public const string PatternDateTime = @$"{PatternDateTimeBasic}{PatternDateTimeExtended}{PatternDateTimeLax}";

    /// <summary>Pattern for japanese mobile phone format.<br/> 11-digit number starting from 0. <br/> (09099999999|090-9999-9999)</summary>
    public const string PatternPhoneMobile = @"0\d0[(-]? (\d{1}[)-]\d{7}|\d{2}[)-]\d{6}|\d{3}[)-]\d{5}|\d{4}[)-]\d{4}|\d{5}[)-]\d{3}|\d{6}[)-]\d{2}|\d{7}[)-]\d{1})";
    /// <summary>Pattern for japanese fix phone format.<br/> 10-digit number starting from 0. The area code is 1 to 5 digits, and the area code and local area code make up the 5-digit number.<br/> (0999999999|09(9999)9999|099-999-9999|0999(99)9999|09999-9-9999|099999()9999|099999--9999|099999-9999)</summary>
    public const string PatternPhoneFix = @"0\d{1}([(-]?\d{4}|\d{1}[(-]?\d{3}|\d{2}[(-]?\d{2}|\d{3}[(-]?\d{1}|\d{4}[(-]?)[)-]?\d{4}";
    /// <summary>Pattern for international phone number format.<br/> (+99999999999)</summary>
    public const string PatternPhoneInternational = @"+\d{1,15}";
    /// <summary>Pattern of phone number.<br/> (japanese mobile or japanese fix or international)</summary>
    public const string PatternPhone = @$"{PatternPhoneMobile}|{PatternPhoneFix}|{PatternPhoneInternational}";  // japanese mobile phone or japanese fix phone or international phone 

    /// <summary>Pattern of email.</summary>
    public const string PatternEmail = @"[\w._%+-]+@[\w.-]+\.[A-Za-z]{2,4}";
    /// <summary>Pattern of url.</summary>
    public const string PatternUrl = @"https?://[\w!\?/\+\-_~=;\.,\*&@#\$%\(\)'\[\]]+";
    /// <summary>Pattern of base64.</summary>
    public const string PatternBase64 = @"[0-9a-zA-Z+/]*={0,2}";


    /// <summary>
    /// Attributes for OpenApiDataType definitions or constraints<br/>
    /// OpenApiDataType 定義 または 制約用の属性
    /// </summary>
    /// <param name="type">Type Format set by OpenApiDataType. <br/>date, time, datetime, phone, email, url as string and have format constraint. <br/><br/>string, integer, number, boolean, date, time, datetime, phone, email, uri</param>
    /// <param name="title">Title</param>
    /// <param name="Summary">summary</param>
    /// <param name="description">Description</param>
    /// <param name="example">Example</param>
    /// <param name="range">Minimam and Maximam set by OpenApiDataType. <br/><br/>Parse to minimum and maximum values as decimal type.<br/>For number and integer.<br/><b>format : </b>min~max<br/><b>example : </b>11~222</param>
    /// <param name="length">MinLength and MaxLength set by OpenApiDataType .<br/><br/>Parse to minimum and maximum length as int type.<br/>For string.<br/><b>format : </b>min~max or fix<br/><b>example : </b>1~2 or 3</param>
    /// <param name="regex">Pattern set by OpenApiDataType. <br/><br/>For string.</param>
    /// <remarks>
    /// https://swagger.io/docs/specification/data-models/data-types/<br/>
    /// Types:<br/>
    /// ・string:  format : (-, date(2017-07-21), time(17:32:28), datetime(2017-07-21T17:32:28Z), password, byte(base64-encoded characters), binary, email, uuid, url, hostname, ipv4, ipv6)<br/>
    /// ・number:  format : (-, float, double)<br/>
    /// ・integer: format : (-, int32, int64)<br/>
    /// ・boolean<br/>
    /// ・array<br/>
    /// ・object<br/>
    /// </remarks>
    public UnitOfSchemaAttribute(
        UnitSchemaType typeformat)
    {
        // type, format
        (Type, Format, Pattern) = typeformat switch
        {
            UnitSchemaType.Int => ("integer", null, null),   // integer.
            UnitSchemaType.Int32 => ("integer", "int32", null),  // signed 32 bits integer.
            UnitSchemaType.Int64 => ("integer", "int64", null),  // signed 64 bits integer.
            UnitSchemaType.Number => ("number", null, null),     // number.
            UnitSchemaType.Float => ("number", "float", null),   // float number.
            UnitSchemaType.Double => ("number", "double", null), // double number.
            UnitSchemaType.Boolean => ("boolean", null, null),   // true or false.
            UnitSchemaType.Date => ("string", "date", PatternDate),              // date string.  9999-19-39
            UnitSchemaType.Time => ("string", "time", PatternTime),              // time string.  29:59:59
            UnitSchemaType.Datetime => ("string", "date-time", PatternDateTime), // datetime string.  9999-19-39T29:59:59Z
            UnitSchemaType.Phone => ("string", "phone", PatternPhone),   // phone number string.
            UnitSchemaType.Email => ("string", "email", PatternEmail),   // email address string.
            UnitSchemaType.Url => ("string", "url", PatternUrl),         // url string.
            UnitSchemaType.Byte => ("string", "byte", PatternBase64),    // base64 encoded characters.
            _ => ("string", null, null) // string.
        };
    }

}



[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateAttribute : Attribute
{
    public UnitOfSchemaValidateAttribute(
        string? errorMessage = "{DisplayName} is invalid value.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateRangeAttribute : Attribute
{
    public UnitOfSchemaValidateRangeAttribute(
        string? errorMessage = "{DisplayName} is invalid range.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateLengthAttribute : Attribute
{
    public UnitOfSchemaValidateLengthAttribute(
        string? errorMessage = "{DisplayName} is invalid length.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public class UnitOfSchemaValidatePatternAttribute : Attribute
{
    public UnitOfSchemaValidatePatternAttribute(
        string pattern,
        string? errorMessage = "{DisplayName} is invalid pattern.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateDateAttribute : Attribute
{
    public UnitOfSchemaValidateDateAttribute(
        string? errorMessage = "{DisplayName} is invalid date.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateTimeAttribute : Attribute
{
    public UnitOfSchemaValidateTimeAttribute(
        string? errorMessage = "{DisplayName} is invalid time.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateDateTimeAttribute : Attribute
{
    public UnitOfSchemaValidateDateTimeAttribute(
        string? errorMessage = "{DisplayName} is invalid datetime.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidatePhoneAttribute : Attribute
{
    public UnitOfSchemaValidatePhoneAttribute(
        string? errorMessage = "{DisplayName} is invalid phone.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateEmailAttribute : Attribute
{
    public UnitOfSchemaValidateEmailAttribute(
        string? errorMessage = "{DisplayName} is invalid email.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateUrlAttribute : Attribute
{
    public UnitOfSchemaValidateUrlAttribute(
        string? errorMessage = "{DisplayName} is invalid url.")
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfSchemaValidateBase64Attribute : Attribute
{
    public UnitOfSchemaValidateBase64Attribute(
        string? errorMessage = "{DisplayName} is invalid base64 string.")
    { }
}



/// <summary>
///// Validate based on UnitOfOasAttribute of UnitOf definition.<br/>
///// UnitOf定義のUnitOfOas属性に基づいて検証する。
///// </summary>
//[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
//public class UnitOfOasValidateAttribute : ValidationAttribute
//{
//    /// <summary>
//    /// Protected virtual method to override and implement validation logic.<br />
//    /// バリデーションロジックをオーバーライドして実装するための保護された仮想メソッド。
//    /// </summary>
//    /// <param name="value">The value to validate.</param>
//    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
//    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
//    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        // 検証不要なら、Success
//        if (value is not IValidatableObject v)
//            return ValidationResult.Success!;
//
//        // Validationを実施
//        var results = v.Validate(validationContext);
//        if (!results.Any())
//            return ValidationResult.Success;
//
//        var msg = ErrorMessage ?? $"Validation for {validationContext.DisplayName} failed!";
//        var compositeResults = new CompositeValidationResult(msg, results.First().MemberNames);
//        compositeResults.AddResults(results);
//        return compositeResults;
//    }
//}




///// <summary>
///// Validation attribute to assert Range.  Used for specifying a range constraint.<br />
///// Range をアサートするバリデーション属性。 範囲制約の指定に使用する。
///// </summary>
//[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
//public class UnitOfRangeAttribute : RangeAttribute, IUnitValidationAttribute
//{
//    /// <summary>
//    /// Constructor that takes integer minimum and maximum values.<br />
//    /// int型の最小値と最大値を取るコンストラクタ。
//    /// </summary>
//    /// <param name="minimum">The minimum value, inclusive</param>
//    /// <param name="maximum">The maximum value, inclusive</param>
//    public UnitOfRangeAttribute(int minimum, int maximum) : base(minimum, maximum) { }
//
//    /// <summary>
//    /// Constructor that takes double minimum and maximum values.<br />
//    /// double型の最小値と最大値をとるコンストラクタです。
//    /// </summary>
//    /// <param name="minimum">The minimum value, inclusive</param>
//    /// <param name="maximum">The maximum value, inclusive</param>
//    public UnitOfRangeAttribute(double minimum, double maximum) : base(minimum, maximum) { }
//
//    /// <summary>
//    /// Constructor that allows for specifying range for arbitrary types. The minimum and maximum strings will be converted to the target type.<br />
//    /// 任意の型の範囲を指定できるコンストラクタ。最小値と最大値の文字列は、対象の型に変換されます。
//    /// </summary>
//    /// <param name="type">The type of the range parameters. Must implement IComparable.</param>
//    /// <param name="minimum">The minimum allowable value.</param>
//    /// <param name="maximum">The maximum allowable value.</param>
//    public UnitOfRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum) { }
//
//    /// <summary>
//    /// Returns true if the value falls between min and max, inclusive.<br />
//    /// 値が min と max の間にある場合、true を返す。
//    /// </summary>
//    /// <param name="value">The value to validate.</param>
//    /// <returns><c>true</c> means the <paramref name="value"/> is valid</returns>
//    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
//    public override bool IsValid(object? value)
//        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetOrDefaultAsObject()));
//}


///// <summary>
///// Validation attribute to assert String Length.   Used for specifying a string length constraint.<br />
///// 文字列の長さを保証するバリデーション属性。  文字列の長さの制約を指定するために使用します。
///// </summary>
//[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
//public class UnitOfStringLengthAttribute : StringLengthAttribute, IUnitValidationAttribute
//{
//    /// <summary>
//    /// Constructor that accepts the maximum length of the string.<br />
//    /// 文字列の最大長を受け付けるコンストラクタ。
//    /// </summary>
//    /// <param name="maximumLength"></param>
//    public UnitOfStringLengthAttribute(int maximumLength) : base(maximumLength) { }
//
//    /// <summary>
//    /// Returns true if the value falls <paramref name="value"/> is valid.<br />
//    /// <paramref name="value"/>の値が有効であれば真を返す。
//    /// </summary>
//    /// <param name="value">The value to validate.</param>
//    /// <returns><c>true</c> means the <paramref name="value"/> is valid.</returns>
//    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
//    public override bool IsValid(object? value)
//        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetOrDefaultAsObject()));
//}


///// <summary>
///// Validate based on attributes of the child object property.<br/>
///// 子オブジェクトのプロパティの属性に基づき検証する。
///// </summary>
//public class NestedValidateAttribute : ValidationAttribute
//{
//    /// <summary>
//    /// Protected virtual method to override and implement validation logic.<br />
//    /// バリデーションロジックをオーバーライドして実装するための保護された仮想メソッド。
//    /// </summary>
//    /// <param name="value">The value to validate.</param>
//    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
//    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
//    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        if (value == null)
//            return ValidationResult.Success;
//
//        var results = new List<ValidationResult>();
//        var context = new ValidationContext(value, null, null);
//        // Validationを実施
//        Validator.TryValidateObject(value, context, results, true);
//
//        if (results.Count == 0)
//            return ValidationResult.Success;
//
//        var validationResults = results.Select(x => new ValidationResult(x.ErrorMessage, x.MemberNames.Select(y => $"{validationContext.DisplayName}.{y}")));
//        var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", new { validationContext.DisplayName }));
//        compositeResults.AddResults(validationResults);
//        return compositeResults;
//    }
//}
