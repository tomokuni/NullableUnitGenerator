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

    public static IEnumerable<ValidationResult> ValidateObject(object? val, UnitOfOasAttribute attr, ValidationContext ctx)
    {
        if (val is null)
            return Enumerable.Empty<ValidationResult>();

        // Validationを実施
        var results = new List<ValidationResult>();
        switch (attr.Type)
        {
            case "string":
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "integer":
                results.Add(ValidateRange(val, attr.Minimum, attr.Maximum, ctx));
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                break;
            case "number":
                results.Add(ValidateRange(val, attr.Minimum, attr.Maximum, ctx));
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                break;
            case "boolean":
                break;
            case "date":
                results.Add(ValidateRange(val, typeof(DateOnly), attr.Minimum, attr.Maximum, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "time":
                results.Add(ValidateRange(val, typeof(TimeOnly), attr.Minimum, attr.Maximum, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "datetime":
                results.Add(ValidateRange(val, typeof(DateTime), attr.Minimum, attr.Maximum, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "phone":
                results.Add(ValidatePhoneNumber(val, ctx));
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "email":
                results.Add(ValidateEmailAddress(val, ctx));
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
            case "uri":
                results.Add(ValidateUrl(val, ctx));
                results.Add(ValidateLength(val.ToString(), attr.MinLength, attr.MaxLength, ctx));
                results.Add(ValidateRegularExpression(val, attr.Pattern, ctx));
                break;
        }

        var validationResult = results.Where(s => s != ValidationResult.Success);
        if (!results.Any())
            return Enumerable.Empty<ValidationResult>();

        return validationResult;
    }



    public static ValidationResult ValidateLength(object? val, int? minLen, int? maxLen, ValidationContext ctx)
    {
        if (val is null || maxLen is null || (minLen is null && maxLen is null))
            return ValidationResult.Success!;

        return (minLen, maxLen) switch
        {
            (not null, not null)
                => new StringLengthAttribute(maxLen.Value) { MinimumLength = minLen.Value }.GetValidationResult(val, ctx)!,
            (null, not null)
                => new StringLengthAttribute(maxLen.Value).GetValidationResult(val, ctx)!,
            _ => ValidationResult.Success!,
        };
    }



    public static ValidationResult ValidateRange(object? val, string? min, string? max, ValidationContext ctx)
    {
        if (val is null)
            return ValidationResult.Success!;

        var type = val.GetType();
        return ValidateRange(val, type, min, max, ctx);
    }



    public static ValidationResult ValidateRange(object? val, Type type, string? min, string? max, ValidationContext ctx)
    {
        if (val is null || string.IsNullOrWhiteSpace(min) || string.IsNullOrWhiteSpace(max))
            return ValidationResult.Success!;

        var value = Convert.ChangeType(val, type);
        return new RangeAttribute(type, min, max).GetValidationResult(value, ctx)!;
    }



    public static ValidationResult ValidateRegularExpression(object? val, string? pattern, ValidationContext ctx)
    {
        if (val is null || string.IsNullOrWhiteSpace(pattern))
            return ValidationResult.Success!;

        var res = new RegularExpressionAttribute(pattern).GetValidationResult(val, ctx)!;
        res.ErrorMessage = res.ErrorMessage;
        return res;
    }



    public static ValidationResult ValidatePhoneNumber(object? val, ValidationContext ctx)
    {
        if (val is null)
            return ValidationResult.Success!;

        return new PhoneAttribute().GetValidationResult(val, ctx)!;
    }



    public static ValidationResult ValidateEmailAddress(object? val, ValidationContext ctx)
    {
        if (val is null)
            return ValidationResult.Success!;

        return new EmailAddressAttribute().GetValidationResult(val, ctx)!;
    }



    public static ValidationResult ValidateUrl(object? val, ValidationContext ctx)
    {
        if (val is null)
            return ValidationResult.Success!;

        return new UrlAttribute().GetValidationResult(val, ctx)!;
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