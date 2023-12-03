using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NullableUnitGenerator;

#pragma warning disable IDE0079 // 不要な抑制を削除します
#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。


/// <summary>
/// UnitOf の Validate 関数群
/// </summary>
public static class UnitValidate
{
    /// <summary>
    /// Validate objects based on UnitOfOasAttribute.<br/>
    /// UnitOfOasAttribute に基づいてオブジェクトの検証を行う。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="attribute">Object of UnitOfOasAttribute.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, Enumerable.Empty.<para>When validation is invalid, an IEnumerater of<see ref="ValidationResult"/>.</para></returns>
    public static IEnumerable<ValidationResult> ValidateObject(object? value, UnitOfOasAttribute attribute, ValidationContext validationContext)
    {
        if (value is null)
            return Enumerable.Empty<ValidationResult>();

        // Validationを実施
        var validationResult = new List<ValidationResult?>
        {
            attribute.ArgType switch
            {
                "phone" => ValidatePhoneNumber(value, validationContext),
                "email" => ValidateEmailAddress(value, validationContext),
                "url" => ValidateUrl(value, validationContext),
                _ => ValidationResult.Success
            },
            attribute.Maximum   is not null ? ValidateRange(value, value.GetType(), attribute.Minimum, attribute.Maximum, validationContext) : ValidationResult.Success,
            attribute.MaxLength is not null ? ValidateLength(value.ToString(), attribute.MinLength, attribute.MaxLength, validationContext) : ValidationResult.Success,
            attribute.Pattern   is not null ? ValidateRegularExpression(value, attribute.Pattern, validationContext) : ValidationResult.Success,
        }.Where(s => s != ValidationResult.Success).Cast<ValidationResult>();

        if (!validationResult.Any())
            return Enumerable.Empty<ValidationResult>();

        return validationResult;
    }


    /// <summary>
    /// Validate the length of the string.<br/>
    /// 文字列の長さを検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="minLen">Minimum length</param>
    /// <param name="maxLen">Maximum length</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateLength(object? value, int? minLen, int? maxLen, ValidationContext validationContext)
    {
        if (value is null || maxLen is null || (minLen is null && maxLen is null))
            return ValidationResult.Success!;

        return (minLen, maxLen) switch
        {
            (not null, not null)
                => new StringLengthAttribute(maxLen.Value) { MinimumLength = minLen.Value }.GetValidationResult(value, validationContext)!,
            (null, not null)
                => new StringLengthAttribute(maxLen.Value).GetValidationResult(value, validationContext)!,
            _ => ValidationResult.Success!,
        };
    }


    /// <summary>
    /// Validate the range of the value.<br/>
    /// 値の範囲を検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateRange(object? value, string? min, string? max, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        var type = value.GetType();
        return ValidateRange(value, type, min, max, validationContext);
    }


    /// <summary>
    /// Validate the range of the value.<br/>
    /// 値の範囲を検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="type">Type of validation target</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateRange(object? value, Type type, string? min, string? max, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(min) || string.IsNullOrWhiteSpace(max))
            return ValidationResult.Success!;

        var val = Convert.ChangeType(value, type);
        return new RangeAttribute(type, min, max).GetValidationResult(val, validationContext)!;
    }


    /// <summary>
    /// Validate the value with a regular expression.<br/>
    /// 正規表現で値を検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="pattern">Regular expression</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateRegularExpression(object? value, string? pattern, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(pattern))
            return ValidationResult.Success!;

        var res = new RegularExpressionAttribute(pattern).GetValidationResult(value, validationContext)!;
        return res;
    }


    /// <summary>
    /// Validate that the value is in telephone number format.<br/>
    /// 値が電話番号形式であることを検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidatePhoneNumber(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        return new PhoneAttribute().GetValidationResult(value, validationContext)!;
    }


    /// <summary>
    /// Validate that the value is in Email address format.<br/>
    /// 値がEmailアドレス形式であることを検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateEmailAddress(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        return new EmailAddressAttribute().GetValidationResult(value, validationContext)!;
    }


    /// <summary>
    /// Validate that the value is in Url format.<br/>
    /// 値がUrl形式であることを検証する。
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A <see cref="ValidationContext"/> instance that provides context about the validation operation, such as the object and member being validated.</param>
    /// <returns>When validation is valid, <see cref="ValidationResult.Success"/>.</returns>
    public static ValidationResult ValidateUrl(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        return new UrlAttribute().GetValidationResult(value, validationContext)!;
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
    private readonly List<ValidationResult> _result = new ();

    /// <summary>Constractor</summary>
    public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
    /// <summary>Constractor</summary>
    public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
    /// <summary>Constractor</summary>
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