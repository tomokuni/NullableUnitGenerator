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


/// <summary>
/// Attribute for defining OpenApiDataType in NullableUnitGenerator<br/>
/// NullableUnitGenerator で OpenApiDataType を定義するための属性
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasAttribute : Attribute
{
    /// <summary>for OpenApiSchema.Type</summary>
    public string Type { get; protected set; }

    /// <summary>for OpenApiSchema.Format</summary>
    public string? Format { get; protected set; }

    /// <summary>for OpenApiSchema.Title</summary>
    public string? Title { get; protected set; }

    /// <summary>for OpenApiSchema.Summary</summary>
    public string? Summary { get; protected set; }

    /// <summary>For OpenApiSchema.Description</summary>
    public string? Description { get; protected set; }

    /// <summary>for OpenApiSchema.Example</summary>
    public object? Example { get; protected set; }

    /// <summary>for OpenApiSchema.Maximum</summary>
    public decimal? Maximum { get; protected set; }

    /// <summary>for OpenApiSchema.ExclusiveMaximum</summary>
    public bool? ExclusiveMaximum { get; protected set; }

    /// <summary>for OpenApiSchema.Minimum</summary>
    public decimal? Minimum { get; protected set; }

    /// <summary>for OpenApiSchema.ExclusiveMinimum</summary>
    public bool? ExclusiveMinimum { get; protected set; }

    /// <summary>for OpenApiSchema.MaxLength</summary>
    public int? MaxLength { get; protected set; }

    /// <summary>for OpenApiSchema.MinLength</summary>
    public int? MinLength { get; protected set; }

    /// <summary>for OpenApiSchema.Pattern</summary>
    public string? Pattern { get; protected set; }

    /// <summary>for OpenApiSchema.Nullable</summary>
    public bool Nullable { get; protected set; }


    /// <summary>Pattern for basic format of date. (yyyyMMdd)</summary>
    public const string PatternDateBasic = @"\d{8}";
    /// <summary>Pattern for extended format of date. (YYYY-MM-DD)</summary>
    public const string PatternDateExtended = @"\d{4}-[01]\d-[03]\d";
    /// <summary>Pattern for lax format of date.</summary>
    public const string PatternDateLax = @"\d{4}[\-\./][01]?\d[\-\./][03]?\d";
    /// <summary>Pattern of date. (basic or extended or lax format.)</summary>
    public const string PatternDate = @$"{PatternDateBasic}|{PatternDateExtended}|{PatternDateLax}";

    /// <summary>Pattern for basic format of time without T. (hhmmss[.sss][Z|+hhmm])</summary>
    public const string PatternTimeBasicWithoutT = @"\d{6}(\.\d+)?(Z|[\+-]\d{4})?";
    /// <summary>Pattern for extended format of time without T. (hh:mm:ss[.sss][Z|+hh:mm])</summary>
    public const string PatternTimeExtendedWithoutT = @"\d{2}:\d{2}:\d{2}(\.\d+)?(Z|[\+-]\d{2}:d{2})?";
    /// <summary>Pattern for lax format of time without T.</summary>
    public const string PatternTimeLaxWithoutT = @"\d?\d:\d?\d:\d?\d(\.\d+)?(Z|[\+-]\d?\d:\d?\d)?";
    /// <summary>Pattern for time without T. (basic or extended or lax format.)</summary>
    public const string PatternTimeWithoutT = @$"{PatternTimeBasicWithoutT}|{PatternTimeExtendedWithoutT}|{PatternTimeLaxWithoutT}";
    /// <summary>Pattern of time. (basic or extended or lax format.)</summary>
    public const string PatternTime = @$"[T]?{PatternTimeWithoutT}";

    /// <summary>Pattern for basic format of datetime. (yyyyMMddThhmmss[.sss][Z|+hhmm])</summary>
    public const string PatternDateTimeBasic = @$"{PatternDateBasic}T{PatternTimeBasicWithoutT}";
    /// <summary>Pattern for extended format of datetime. ((YYYY-MM-DDThh:mm:ss[.sss][Z|+hh:mm])</summary>
    public const string PatternDateTimeExtended = @$"{PatternDateExtended}T{PatternTimeExtendedWithoutT}";
    /// <summary>Pattern for lax format of datetime.</summary>
    public const string PatternDateTimeLax = @$"{PatternDateLax}[T ]{PatternTimeLaxWithoutT}";  // lax format.
    /// <summary>Pattern of datetime. (basic or extended or lax format.)</summary>
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

    /// <summary>is signed integer</summary>
    public static bool IsIntegerSigned(object value) => value is byte || value is short || value is int || value is long;
    /// <summary>is unsigned integer</summary>
    public static bool IsIntegerUnsigned(object value) => value is sbyte || value is ushort || value is uint || value is ulong;
    /// <summary>is integer</summary>
    public static bool IsInteger(object value) => IsIntegerSigned(value) || IsIntegerUnsigned(value);
    /// <summary>is float</summary>
    public static bool IsFloat(object value) => value is float | value is double | value is decimal;
    /// <summary>is numeric</summary>
    public static bool IsNumeric(object value) => IsInteger(value) || IsFloat(value);

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
    public UnitOfOasAttribute(
        string? typeformat = null,
        string? title = null,
        string? summary = null,
        string? description = null,
        object? example = null,
        bool nullable = true,
        object? maximum = null,         // decimal value
        bool exclusiveMaximum = false,
        object? minimum = null,         // decimal value
        bool exclusiveMinimum = false,
        object? maxLength = null,       // int value
        object? minLength = null,       // int value
        string? pattern = null,
        string? errorMessage = "{DisplayName} value is invalid.")
    {
        // type, format
        (Type, Format) = typeformat switch
        {
            "integer" => ("integer", null),         // integer.
            "int32" => ("integer", "int32"),        // signed 32 bits integer.
            "int64" => ("integer", "int64"),        // signed 64 bits integer.
            "number" => ("number", null),           // number.
            "float" => ("number", "float"),         // float number.
            "double" => ("number", "double"),       // double number.
            "boolean" => ("boolean", null),         // true or false.
            "date" => ("string", "date"),           // date string.  9999-19-39
            "time" => ("string", "time"),           // time string.  29:59:59
            "datetime" => ("string", "date-time"),  // datetime string.  9999-19-39T29:59:59Z
            "phone" => ("string", "phone"),         // phone number string.
            "email" => ("string", "email"),         // email address string.
            "url" => ("string", "url"),             // url string.
            "byte" => ("string", "byte"),           // base64 encoded characters.
            "pattern" => ("string", null),          // regex pattern.
            _ => ("string", null)                   // string.
        };

        var isNumericMaximum = decimal.TryParse($"{maximum}", out var decimalMaximum);
        var isNumericMinimum = decimal.TryParse($"{minimum}", out var decimalMinimum);
        var isIntMaxLength = int.TryParse($"{maxLength}", out var intMaxLength);
        var isIntMinLength = int.TryParse($"{minLength}", out var intMinLength);

        if (!isNumericMaximum)
            throw new ArgumentException($"maximum {maximum} is not decimal value.");
        if (!isNumericMinimum)
            throw new ArgumentException($"minimum {minimum} is not decimal value.");
        if (!isIntMaxLength)
            throw new ArgumentException($"maxLength {maxLength} is not int value.");
        if (!isIntMinLength)
            throw new ArgumentException($"minLength {minLength} is not int value.");

        Title = string.IsNullOrEmpty(title) ? null : title;
        Summary = string.IsNullOrEmpty(summary) ? null : summary;
        Description = string.IsNullOrEmpty(description) ? null : description;
        Example = example;
        Nullable = nullable;
        Maximum = decimalMaximum;
        ExclusiveMaximum = exclusiveMaximum ? true : null;
        Minimum = decimalMinimum;
        ExclusiveMinimum = exclusiveMinimum ? true : null;
        MaxLength = intMaxLength;
        MinLength = intMinLength;
        Pattern = string.IsNullOrEmpty(pattern) ? null : pattern;
    }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasRangeAttribute : UnitOfOasAttribute
{
    public UnitOfOasRangeAttribute(
        object? maximum = null, bool exclusiveMaximum = false,
        object? minimum = null, bool exclusiveMinimum = false,
        string? errorMessage = "{DisplayName} is invalid range.")
            : base(errorMessage: errorMessage,
                   maximum: maximum, exclusiveMaximum: exclusiveMaximum,
                   minimum: minimum, exclusiveMinimum: exclusiveMinimum)
    {
    }

    public UnitOfOasRangeAttribute(
        string range,
        string? errorMessage = "{DisplayName} is invalid range.")
            : base(errorMessage: errorMessage)
    {
        const string patternDecimalPointNumber = @"[\-\+]?\d{1,3}(,?\d{3})*(\.\d+)";
        const string patternRange = @$"^(?<min>{patternDecimalPointNumber})(?<minInequality><=|<)?~(?<maxInequality><=|<)?(?<max>{patternDecimalPointNumber})$";

        Match match = Regex.Match(range, patternRange);
        if (!match.Success)
            throw new ArgumentException(@$"renge {range} is invalid value.");

        string min = match.Groups["min"].Value.Replace(",", "");
        string minInequality = match.Groups["minInequality"].Value;
        string maxInequality = match.Groups["maxInequality"].Value;
        string max = match.Groups["max"].Value.Replace(",", "");
        if (!IsNumeric(min))
            throw new ArgumentException($"minimum {min} is invalid value.");
        if (!IsNumeric(max))
            throw new ArgumentException($"maximum {max} is invalid value.");

        Maximum = string.IsNullOrEmpty(max) ? null : decimal.Parse(max);
        Minimum = string.IsNullOrEmpty(min) ? null : decimal.Parse(min);
        ExclusiveMaximum = minInequality == "<" ? true : null;
        ExclusiveMinimum = maxInequality == "<" ? true : null;
    }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasLengthAttribute : UnitOfOasAttribute
{
    public UnitOfOasLengthAttribute(
        int? maxLength = null, int? minLength = null,
        string? errorMessage = "{DisplayName} is invalid length.")
            : base(errorMessage: errorMessage,
                   maxLength: maxLength, minLength: minLength)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public class UnitOfOasPatternAttribute : UnitOfOasAttribute
{
    public UnitOfOasPatternAttribute(
        string pattern,
        string? errorMessage = "{DisplayName} is invalid pattern.")
            : base(errorMessage: errorMessage, 
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasDateAttribute : UnitOfOasAttribute
{
    public UnitOfOasDateAttribute(
        string pattern = PatternDate,
        string? errorMessage = "{DisplayName} is invalid date.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasTimeAttribute : UnitOfOasAttribute
{
    public UnitOfOasTimeAttribute(
        string pattern = PatternTime,
        string? errorMessage = "{DisplayName} is invalid time.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasDateTimeAttribute : UnitOfOasAttribute
{
    public UnitOfOasDateTimeAttribute(
        string pattern = PatternDateTime,
        string? errorMessage = "{DisplayName} is invalid datetime.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasPhoneAttribute : UnitOfOasAttribute
{
    public UnitOfOasPhoneAttribute(
        string pattern = PatternPhone,
        string? errorMessage = "{DisplayName} is invalid phone.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasEmailAttribute : UnitOfOasAttribute
{
    public UnitOfOasEmailAttribute(
        string pattern = PatternEmail,
        string? errorMessage = "{DisplayName} is invalid email.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasUrlAttribute : UnitOfOasAttribute
{
    public UnitOfOasUrlAttribute(
        string pattern = PatternUrl,
        string? errorMessage = "{DisplayName} is invalid url.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class UnitOfOasBase64Attribute : UnitOfOasAttribute
{
    public UnitOfOasBase64Attribute(
        string pattern = PatternBase64,
        string? errorMessage = "{DisplayName} is invalid base64 string.")
            : base(errorMessage: errorMessage,
                   pattern: pattern)
    { }
}



/// <summary>
/// Validate based on UnitOfOasAttribute of UnitOf definition.<br/>
/// UnitOf定義のUnitOfOas属性に基づいて検証する。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfOasValidateAttribute : ValidationAttribute
{
    /// <summary>
    /// Protected virtual method to override and implement validation logic.<br />
    /// バリデーションロジックをオーバーライドして実装するための保護された仮想メソッド。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // 検証不要なら、Success
        if (value is not IValidatableObject v)
            return ValidationResult.Success!;

        // Validationを実施
        var results = v.Validate(validationContext);
        if (!results.Any())
            return ValidationResult.Success;

        var msg = ErrorMessage ?? $"Validation for {validationContext.DisplayName} failed!";
        var compositeResults = new CompositeValidationResult(msg, results.First().MemberNames);
        compositeResults.AddResults(results);
        return compositeResults;
    }
}




/// <summary>
/// Validation attribute to assert Range.  Used for specifying a range constraint.<br />
/// Range をアサートするバリデーション属性。 範囲制約の指定に使用する。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfRangeAttribute : RangeAttribute, IUnitValidationAttribute
{
    /// <summary>
    /// Constructor that takes integer minimum and maximum values.<br />
    /// int型の最小値と最大値を取るコンストラクタ。
    /// </summary>
    /// <param name="minimum">The minimum value, inclusive</param>
    /// <param name="maximum">The maximum value, inclusive</param>
    public UnitOfRangeAttribute(int minimum, int maximum) : base(minimum, maximum) { }

    /// <summary>
    /// Constructor that takes double minimum and maximum values.<br />
    /// double型の最小値と最大値をとるコンストラクタです。
    /// </summary>
    /// <param name="minimum">The minimum value, inclusive</param>
    /// <param name="maximum">The maximum value, inclusive</param>
    public UnitOfRangeAttribute(double minimum, double maximum) : base(minimum, maximum) { }

    /// <summary>
    /// Constructor that allows for specifying range for arbitrary types. The minimum and maximum strings will be converted to the target type.<br />
    /// 任意の型の範囲を指定できるコンストラクタ。最小値と最大値の文字列は、対象の型に変換されます。
    /// </summary>
    /// <param name="type">The type of the range parameters. Must implement IComparable.</param>
    /// <param name="minimum">The minimum allowable value.</param>
    /// <param name="maximum">The maximum allowable value.</param>
    public UnitOfRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum) { }

    /// <summary>
    /// Returns true if the value falls between min and max, inclusive.<br />
    /// 値が min と max の間にある場合、true を返す。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns><c>true</c> means the <paramref name="value"/> is valid</returns>
    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
    public override bool IsValid(object? value)
        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetOrDefaultAsObject()));
}


/// <summary>
/// Validation attribute to assert String Length.   Used for specifying a string length constraint.<br />
/// 文字列の長さを保証するバリデーション属性。  文字列の長さの制約を指定するために使用します。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UnitOfStringLengthAttribute : StringLengthAttribute, IUnitValidationAttribute
{
    /// <summary>
    /// Constructor that accepts the maximum length of the string.<br />
    /// 文字列の最大長を受け付けるコンストラクタ。
    /// </summary>
    /// <param name="maximumLength"></param>
    public UnitOfStringLengthAttribute(int maximumLength) : base(maximumLength) { }

    /// <summary>
    /// Returns true if the value falls <paramref name="value"/> is valid.<br />
    /// <paramref name="value"/>の値が有効であれば真を返す。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns><c>true</c> means the <paramref name="value"/> is valid.</returns>
    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
    public override bool IsValid(object? value)
        => (value is IUnitOf v) && (!v.HasValue || v.HasValue && base.IsValid(v.GetOrDefaultAsObject()));
}


/// <summary>
/// Validate based on attributes of the child object property.<br/>
/// 子オブジェクトのプロパティの属性に基づき検証する。
/// </summary>
public class NestedValidateAttribute : ValidationAttribute
{
    /// <summary>
    /// Protected virtual method to override and implement validation logic.<br />
    /// バリデーションロジックをオーバーライドして実装するための保護された仮想メソッド。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    /// <exception cref="InvalidOperationException"> is thrown if the current attribute is malformed.</exception>
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
