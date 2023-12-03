using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace NullableUnitGenerator.Extensions;


/// <summary>
/// NullableUnitGeneratorExtensions
/// </summary>
public static class NullableUnitGeneratorExtensions
{

    /// <summary>
    /// Convert word to Pascal case.<br/>
    /// 単語をパスカルケースに変換する。<br/>
    /// </summary>
    /// <param name="str">source word<br/>変換元の単語</param>
    /// <returns>Converted word<br/>変換後の単語</returns>
    public static string Pascalize(string str)
    {
        var w = regexPascalize.Replace(str,
            m => char.ToUpper(m.Groups[1].Value[0]) + m.Groups[1].Value[1..].ToLower() + m.Groups[2].Value);
        return char.ToUpper(w[0]) + w[1..];
    }
    static readonly Regex regexPascalize = new(@"(^[A-Z][A-Z0-9]*|[A-Z][A-Z0-9]+)($|[A-Z][a-z0-9])");


    /// <summary>
    /// Convert string to Pascal case.<br/>
    /// 文字列をパスカルケースに変換する。<br/>
    /// </summary>
    /// <param name="str">source word<br/>変換元の単語</param>
    /// <returns>Converted word<br/>変換後の単語</returns>
    public static string ToPascalCase(this string str)
    {
        var words = str
            .Split(new[] { "_", "-", " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(Pascalize)
            .ToArray();
        return string.Join(string.Empty, words);
    }


    /// <summary>
    /// Convert string to CamelCase.<br/>
    /// 文字列をキャメルケースに変換する。<br/>
    /// </summary>
    /// <param name="str">source word<br/>変換元の単語</param>
    /// <returns>Converted word<br/>変換後の単語</returns>
    public static string ToCamelCase(this string str)
    {
        var pascal = ToPascalCase(str);
        return char.ToLower(pascal[0]) + pascal[1..];
    }


    /// <summary>
    /// Convert string to snake case<br/>
    /// 文字列をスネークケースに変換する。<br/>
    /// </summary>
    /// <param name="str">source word<br/>変換元の単語</param>
    /// <param name="delimiter">delimiter<br/>区切り文字</param>
    /// <returns>Converted word<br/>変換後の単語</returns>
    public static string ToSnakeCase(this string str, string delimiter = "_")
    {
        var s0 = ToCamelCase(str);
        var s1 = regexSnakeSplit.Replace(s0, ("$1" + delimiter + "$2"));
        return s1.ToLower();
    }
    static readonly Regex regexSnakeSplit = new(@"([a-z0-9])([A-Z])");


    /// <summary>
    /// Convert ISO8601 extended date/time string to DateTimeOffset.<br/>
    /// ISO8601拡張 の日時文字列を DateTimeOffset に変換する。<br/>
    /// </summary>
    /// <param name="datetimeString">source date and time string<br/>変換元の日時</param>
    /// <returns>Converted DateTimeOffset<br/>変換後の DateTimeOffset</returns>
    public static DateTimeOffset ToDateTimeOffset(this string datetimeString)
        => TimeZoneInfo.ConvertTime(DateTimeOffset.Parse(datetimeString), TimeZoneInfo.Local);
    //static readonly TimeZoneInfo tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");


    /// <summary>
    /// Convert ISO8601 extended date/time string to DateTime.<br/>
    /// ISO8601拡張 の日時文字列を DateTime に変換する。<br/>
    /// </summary>
    /// <param name="datetimeString">source date and time string<br/>変換元の日時</param>
    /// <returns>Converted DateTime<br/>変換後の DateTime</returns>
    public static DateTime ToDateTime(this string datetimeString)
        => datetimeString.ToDateTimeOffset().DateTime;


    /// <summary>
    /// Convert ISO8601 extended date/time string to DateOnly.<br/>
    /// ISO8601拡張 の日付文字列を DateOnly に変換する。<br/>
    /// </summary>
    /// <param name="dateString">source date and time string<br/>変換元の日時文字列</param>
    /// <returns>Converted DateOnly<br/>変換後の DateOnly</returns>
    public static DateOnly ToDateOnly(this string dateString)
        => DateOnly.FromDateTime(dateString.ToDateTime());


    /// <summary>
    /// Convert ISO8601 extended date/time string to TimeOnly.<br/>
    /// ISO8601拡張 の時刻文字列を TimeOnly に変換する。<br/>
    /// </summary>
    /// <param name="timeString">source date and time string<br/>変換元の時刻文字列</param>
    /// <returns>Converted TimeOnly<br/>変換後の TimeOnly</returns>
    public static TimeOnly ToTimeOnly(this string timeString)
        => TimeOnly.FromDateTime(timeString.ToDateTime());


    /// <summary>
    /// Convert ISO8601 duration string to TimeSpan.<br/>
    /// ISO8601 の間隔文字列を TimeSpan に変換する。<br/>
    /// </summary>
    /// <param name="timespanString">source duration string<br/>変換元の間隔文字列</param>
    /// <returns>Converted TimeSpan<br/>変換後の TimeSpan</returns>
    public static TimeSpan ToTimeSpan(this string timespanString)
    {
        if (timespanString.Trim().StartsWith("P"))
            return ToTimeSpanFromIsoDurationString(timespanString);
        return ToTimeSpanFromTimeSpanString(timespanString);
    }


    /// <summary>
    /// Converts ISO8601 time duration string to TimeSpan.<br/>
    /// ISO8601 の時間間隔文字列を TimeSpan に変換する。<br/><br/>
    ///    <b>PnYnMnDTnHnMnS</b><br/><b>P(date)T(time)</b><br/>
    ///    P は期間を表す指定子（period を表す）であり、継続時間表現の先頭に置かれる。<br/>
    ///    T 時間の指定子であり、継続時間表現の時間の部分の前に置く。<br/>
    ///    (date) は Y M D で、それぞれ 年 月 日 の指定子であり、年 月 日 を表す数値のあとに置かれる。<br/>
    ///    (time) は H M S で、それぞれ 時 分 秒 の指定子であり、時 分 秒 を表す数値のあとに置かれる。<br/>
    ///    指定子を含む日付と時間の要素は、その値が0の時には省略することができる。<br/>
    /// </summary>
    /// <param name="timespanString">source duration string<br/>変換元の時刻文字列</param>
    /// <returns>Converted TimeSpan<br/>変換後の TimeSpan</returns>
    public static TimeSpan ToTimeSpanFromIsoDurationString(string timespanString)
        => XmlConvert.ToTimeSpan(timespanString);


    /// <summary>
    /// Converts a string to TimeSpan.<br/>
    /// 文字列を TimeSpan に変換する。<br/><br/>
    /// <b>1 number  => d | d.h</b><br/>
    /// <b>2 numbers => h:m | d.h:m</b><br/>
    /// <b>3 numbers => h:m:s | h:m:.f | h:m:s.f | d.h:m:s | d.h:m:.f |d.h:m:s.f</b>
    /// </summary>
    /// <example>
    /// 1 number  => "0" to "1.00:00:00" | "1.2"  to "1.02:00:00"<br/>
    /// 2 numbers => "2:3" to "02:03:00" | "1.2:3" to "1.02:03:00"<br/>
    /// 3 numbers => "2:3:4" to "02:03:04" | "2:3:.9" to "02:03:00.9" | "2:3:4.9" to "02:03:04.9"
    ///              | "1.2:3:4" to "1.02:03:04" | "1.2:3:.9" to "1.02:03:00.9" | "1.2:3:4.9" to "1.02.03:04.9"
    /// </example>
    /// <param name="timespanString">source duration string<br/>変換元の時刻文字列</param>
    /// <returns>Converted TimeSpan<br/>変換後の TimeSpan</returns>
    /// <exception cref="FormatException">書式エラー</exception>
    public static TimeSpan ToTimeSpanFromTimeSpanString(string timespanString)
    {
        var trim = timespanString.Trim();
        if (!regexTimeSpan.IsMatch(trim))
            throw new FormatException($"The string '{trim}' is not a valid TimeSpan value.");

        var isNegative = trim.StartsWith("-"); // "-1:2:3" is true
        var hms = isNegative ? trim[1..].Split(':') : trim.Split(':');
        var dh = hms[0].Contains(".") ? hms[0].Split('.') : (hms.Length >= 2) ? new[] { "", hms[0] } : new[] { hms[0], "" };
        var m = hms.Length >= 2 ? hms[1] : "";
        var sf = hms.Length >= 3 ? (hms[2].Contains(".") ? hms[2].Split('.') : new[] { hms[2], "" }) : new[] { "", "" };

        var intD = int.Parse("0" + dh[0]);
        var intH = int.Parse("0" + dh[1]);
        var intM = int.Parse("0" + m);
        var intS = int.Parse("0" + sf[0]);
        var intTick = int.Parse("0" + sf[1].ToString().PadRight(7, '0'));

        var ts = new TimeSpan(intD, intH, intM, intS) + new TimeSpan(intTick);
        return isNegative ? ts.Negate() : ts;
    }
    static readonly Regex regexTimeSpan = new(@"^-?\d+(\.\d+)?$|^-?\d+(\.\d+)?:\d+$|^-?\d+(\.\d+)?:\d+:(\d+|\.\d+|\d+\.\d+)?$");


    /// <summary>
    /// Convert DateTime to ISO8601 extended string.<br/>
    /// DateTime を ISO8601拡張 文字列に変換する。<br/>
    /// </summary>
    /// <param name="datetime">source DateTime<br/>変換元の日時</param>
    /// <returns>Converted string<br/>変換後の日時</returns>
    public static string ToIsoString(this DateTime datetime)
        => new DateTime(datetime.Ticks, datetime.Kind == DateTimeKind.Unspecified ? DateTimeKind.Local : datetime.Kind)
            .ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFzzz");


    /// <summary>
    /// Convert DateOnly to ISO8601 extended string.<br/>
    /// DateOnly を ISO8601拡張 文字列に変換する。<br/>
    /// </summary>
    /// <param name="date">source DateOnly<br/>変換元の日付</param>
    /// <param name="withOffset">TimeZoneOffsetを付加するか</param>
    /// <returns>Converted string<br/>変換後の日付</returns>
    public static string ToIsoString(this DateOnly date, bool withOffset = false)
        => date.ToString("yyyy-MM-dd") + (withOffset ? stringZzz : "");
    static readonly string stringZzz = new DateTime(0, DateTimeKind.Local).ToString("zzz");


    /// <summary>
    /// Convert TimeOnly to ISO8601 extended string.<br/>
    /// TimeOnly を ISO8601拡張 文字列に変換する。<br/>
    /// </summary>
    /// <param name="time">source TimeOnly<br/>変換元の時刻</param>
    /// <param name="withOffset">TimeZoneOffsetを付加するか</param>
    /// <returns>Converted string<br/>変換後の時刻</returns>
    public static string ToIsoString(this TimeOnly time, bool withOffset = false)
        => time.ToString("hh:mm:ss.FFFFFFF") + (withOffset ? stringZzz : "");


    /// <summary>
    /// Convert TimeSpan to duration string in time format.<br/>
    /// TimeSpan を 時刻形式の時間間隔文字列に変換する。<br/>
    /// </summary>
    /// <param name="timespan">source TimeSpan<br/>変換元の TimeSpan</param>
    /// <param name="enableDay">日の出力方法： true: 日付を含む d.hh 形式で出力、 false: 時間の積算形式 [hh] で出力</param>
    /// <returns>Converted time format string<br/>変換後の TimeSpan文字列</returns>
    public static string ToHmsString(this TimeSpan timespan, bool enableDay = true)
        => enableDay
            ? $"{timespan.Ticks:'';'-';''}{timespan.Days:0'.';0'.';''}{timespan:hh':'mm':'ss'.'FFFFFFF}".TrimEnd('.')
            : $"{(int)timespan.TotalHours:00}{timespan:':'mm':'ss'.'FFFFFFF}".TrimEnd('.');


    /// <summary>
    /// Convert TimeSpan to ISO8601 extended string.<br/>
    /// TimeSpan を ISO8601 時間間隔文字列に変換する。<br/>
    /// </summary>
    /// <param name="timespan">source TimeSpan<br/>変換元の TimeSpan</param>
    /// <returns>Converted duration string<br/>変換後の duration 文字列</returns>
    public static string ToIsoString(this TimeSpan timespan)
        => XmlConvert.ToString(timespan);

}

