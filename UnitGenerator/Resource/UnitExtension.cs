using System;
using System.Linq;
using System.Text.RegularExpressions;

#if !UGO_OPENAPI_DISABLE
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
#endif

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGeneratorExtensions
/// </summary>
public static class NullableUnitGeneratorExtensions
{
#if !UGO_OPENAPI_DISABLE
    // UnitOfOpenApiDataType属性を元に、SwaggerGenOptions に OpenApiSchema を設定する。

    /// <summary>
    /// UnitOfOas 属性を探索し、SwaggerGenOptions に Schema 情報として登録する
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static SwaggerGenOptions MapTypeUnitOfOas(this SwaggerGenOptions options)
    {
        // UnitOfOas 属性が付与されたクラスと属性を取得
        var ta = UnitHelper.GetTypeAndAttributes<UnitOfOasAttribute>();
        foreach (var (type, attr) in ta)
        {
            options.MapType(type, attr.ToOpenApiSchema);
        }
        return options;
    }


    /// <summary>
    /// UnitOfOas 属性を元に、OpenApiSchema オブジェクトに変換する。
    /// </summary>
    /// <param name="attr"></param>
    /// <returns></returns>
    public static OpenApiSchema ToOpenApiSchema(this UnitOfOasAttribute attr)
    {
        IOpenApiAny exampleAny = attr.Example switch
        {
            null => new OpenApiNull(),
            int integer => new OpenApiInteger(integer),
            double floating => new OpenApiDouble(floating),
            var e => new OpenApiString(e.ToString()),
        };

        var schema = new OpenApiSchema
        {
            Type = attr.Type,
            Format = attr.Format,
            Minimum = attr.Minimum is null ? null : decimal.Parse(attr.Minimum),
            Maximum = attr.Maximum is null ? null : decimal.Parse(attr.Maximum),
            MinLength = attr.MinLength,
            MaxLength = attr.MaxLength,
            Pattern = attr.Pattern,
            Example = exampleAny,
            Nullable = attr.Nullable,
        };

        return schema;
    }
#endif



    /// <summary>
    /// 文字列をパスカルケースに変換する
    /// </summary>
    /// <param name="str">変換元文字列</param>
    /// <returns>パスカルケースの文字列</returns>
    public static string ToPascalCase(this string str)
    {
        var words = str
            .Split(new[] { "_", "-", " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(UnitHelper.Pascalize)
            .ToArray();
        return string.Join(string.Empty, words);
    }


    /// <summary>
    /// 文字列をキャメルケースに変換する
    /// </summary>
    /// <param name="str">変換元文字列</param>
    /// <returns>キャメルケースの文字列</returns>
    public static string ToCamelCase(this string str)
    {
        var pascal = ToPascalCase(str);
        return char.ToLower(pascal[0]) + pascal[1..];
    }


    /// <summary>
    /// 文字列をスネークケースに変換する
    /// </summary>
    /// <param name="str">変換元文字列</param>
    /// <param name="delimiter">区切り文字</param>
    /// <returns>スネークケースの文字列</returns>
    public static string ToSnakeCase(this string str, string delimiter = "_")
    {
        var s0 = ToCamelCase(str);
        var s1 = Regex.Replace(s0, @"([a-z0-9])([A-Z])", ("$1" + delimiter + "$2"));
        return s1.ToLower();
    }


    /// <summary>
    /// 文字列を日時に変換する。
    /// </summary>
    /// <param name="datetimeString">変換元の日時文字列</param>
    /// <returns>変換後の日時</returns>
    public static DateTimeOffset ToDateTimeOffset(this string datetimeString)
        => TimeZoneInfo.ConvertTime(DateTimeOffset.Parse(datetimeString), TimeZoneInfo.Local);
    //static readonly TimeZoneInfo tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");


    /// <summary>
    /// 日時をISO8601文字列に変換する
    /// </summary>
    /// <param name="datetimeoffset">変換元の日時</param>
    /// <returns>変換後の日時</returns>
    public static string ToJsonString(this DateTimeOffset datetimeoffset)
        => datetimeoffset.ToString("yyyy-MM-ddTHH:MM:ss.FFFFFFFzzz");


    /// <summary>
    /// 文字列を日時に変換する。
    /// </summary>
    /// <param name="datetimeString">変換元の日時文字列</param>
    /// <returns>変換後の日時</returns>
    public static DateTime ToDateTime(this string datetimeString)
        => ToDateTimeOffset(datetimeString).DateTime;


    /// <summary>
    /// 日時をISO8601文字列に変換する
    /// </summary>
    /// <param name="datetime">変換元の日時</param>
    /// <returns>変換後の日時</returns>
    public static string ToJsonString(this DateTime datetime)
        => new DateTime(datetime.Ticks, datetime.Kind == DateTimeKind.Unspecified ? DateTimeKind.Local : datetime.Kind)
            .ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFzzz");


    /// <summary>
    /// 文字列を日付に変換する。
    /// </summary>
    /// <param name="dateString">変換元の日時文字列</param>
    /// <returns>変換後の日付</returns>
    public static DateOnly ToDateOnly(this string dateString)
        => DateOnly.FromDateTime(ToDateTime(dateString));


    /// <summary>
    /// 日付をISO8601文字列に変換する
    /// </summary>
    /// <param name="date">変換元の日付</param>
    /// <returns>変換後の日付</returns>
    public static string ToJsonString(this DateOnly date)
        => date.ToString("yyyy-MM-dd");


    /// <summary>
    /// 文字列を時刻に変換する。
    /// </summary>
    /// <param name="timeString">変換元の日時文字列</param>
    /// <returns>変換後の時刻</returns>
    public static TimeOnly ToTimeOnly(this string timeString)
        => TimeOnly.FromDateTime(ToDateTime(timeString));


    /// <summary>
    /// 時刻をISO8601文字列に変換する
    /// </summary>
    /// <param name="time">変換元の時刻</param>
    /// <returns>変換後の時刻</returns>
    public static string ToJsonString(this TimeOnly time)
        => time.ToString("hh:mm:ss.FFFFFFF");


    /// <summary>
    /// 文字列を TimeSpan に変換する。
    ///    1 number  => d | d.h                         "0" to "1.00:00:00" | "1.2"  to "1.02:00:00"
    ///    2 numbers => h:m | d.h:m                     "2:3" to "02:03:00" | "1.2:3" to "1.02:03:00"
    ///    3 numbers => h:m:s | h:m:.f | h:m:s.f        "2:3:4" to "02:03:04" | "2:3:.9" to "02:03:00.9" | "2:3:4.9" to "02:03:04.9"
    ///               | d.h:m:s | d.h:m:.f |d.h:m:s.f   "1.2:3:4" to "1.02:03:04" | "1.2:3:.9" to "1.02:03:00.9" | "1.2:3:4.9" to "1.02.03:04.9"
    /// </summary>
    /// <param name="timespanString">変換元の時刻文字列</param>
    /// <returns>変換後の TimeSpan</returns>
    public static TimeSpan ToTimeSpan(this string timespanString)
    {
        var trim = timespanString.Trim();
        var isNegative = trim.StartsWith("-"); // "-1:2:3" is true
        if (isNegative)
            trim = trim[1..];
        var s = trim.Split(':');
        var s0 = s[0].Split('.').Select(s => long.Parse(s));

        int days = s[0].Contains(".") ? int.Parse(s[0].Split('.')[0]) : (s.Length == 1 ? int.Parse("0" + s[0]) : 0);
        int hours = s[0].Contains(".") ? int.Parse(s[0].Split('.')[1]) : (s.Length >= 2 ? int.Parse("0" + s[0]) : 0);
        int minutes = s.Length >= 2 ? int.Parse("0" + s[1]) : 0;
        int seconds = s.Length >= 3 ? (s[2].Contains(".") ? int.Parse("0" + s[2].Split('.')[0]) : int.Parse("0" + s[2])) : 0;
        int nanoseconds = s.Length >= 3 ? (s[2].Contains(".") ? int.Parse(s[2].Split('.')[1].PadRight(7,'0')) : 0) : 0;

        var ts = new TimeSpan(days, hours, minutes, seconds) + new TimeSpan(nanoseconds);
        if (isNegative)
            ts = ts.Negate();
        return ts;
    }


    /// <summary>
    /// TimeSpan を文字列に変換する
    /// </summary>
    /// <param name="timespan">変換元の TimeSpan</param>
    /// <param name="enableDay">日の出力方法： true: d.hh 形式で出力、 false: 時間の積算形式で出力</param>
    /// <returns>変換後の TimeSpan文字列</returns>
    public static string ToJsonString(this TimeSpan timespan, bool enableDay = true)
        => enableDay
            ? $"{timespan.Ticks:'';'-';''}{timespan.Days:0'.';0'.';''}{timespan:hh':'mm':'ss'.'FFFFFFF}".TrimEnd('.')
            : $"{(int)timespan.TotalHours:00}{timespan:':'mm':'ss'.'FFFFFFF}".TrimEnd('.');

}

