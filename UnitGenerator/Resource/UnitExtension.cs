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
    /// 単語をパスカルケースに変換する
    /// </summary>
    /// <param name="str">変換元の単語</param>
    /// <returns>変換後の単語</returns>
    public static string Pascalize(string str)
    {
        var w = regexPascalize.Replace(str,
            m => char.ToUpper(m.Groups[1].Value[0]) + m.Groups[1].Value[1..].ToLower() + m.Groups[2].Value);
        return char.ToUpper(w[0]) + w[1..];
    }
    static readonly Regex regexPascalize = new(@"(^[A-Z][A-Z0-9]*|[A-Z][A-Z0-9]+)($|[A-Z][a-z0-9])");


    /// <summary>
    /// 文字列をパスカルケースに変換する
    /// </summary>
    /// <param name="str">変換元文字列</param>
    /// <returns>パスカルケースの文字列</returns>
    public static string ToPascalCase(this string str)
    {
        var words = str
            .Split(new[] { "_", "-", " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(Pascalize)
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
        var s1 = regexSnakeSplit.Replace(s0, ("$1" + delimiter + "$2"));
        return s1.ToLower();
    }
    static readonly Regex regexSnakeSplit = new(@"([a-z0-9])([A-Z])");


    /// <summary>
    /// ISO8601拡張 の日時文字列を DateTimeOffset に変換する
    /// </summary>
    /// <param name="datetimeString">変換元の日時</param>
    /// <returns>変換後の日時</returns>
    public static DateTimeOffset ToDateTimeOffset(this string datetimeString)
        => TimeZoneInfo.ConvertTime(DateTimeOffset.Parse(datetimeString), TimeZoneInfo.Local);
    //static readonly TimeZoneInfo tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");


    /// <summary>
    /// ISO8601拡張 の日時文字列を DateTime に変換する
    /// </summary>
    /// <param name="datetimeString">変換元の日時</param>
    /// <returns>変換後の日時</returns>
    public static DateTime ToDateTime(this string datetimeString)
        => datetimeString.ToDateTimeOffset().DateTime;


    /// <summary>
    /// 文字列を TimeSpan に変換する。
    /// </summary>
    /// <param name="timespanString">変換元の時刻文字列</param>
    /// <returns>変換後の TimeSpan</returns>
    public static TimeSpan ToTimeSpan(this string timespanString)
    {
        if (string.IsNullOrWhiteSpace(timespanString.Trim()))
            return TimeSpan.Zero;

        if (timespanString.Trim().StartsWith("P"))
            return ToTimeSpanFromIsoDurationString(timespanString);
        return ToTimeSpanFromTimeSpanString(timespanString);
    }

    /// <summary>
    /// ISO8601 の期間文字列を TimeSpan に変換する。<br/><br/>
    ///    <b>P[n]Y[n]M[n]DT[n]H[n]M[n]S</b><br/>
    ///    最初の1文字目は、"Period" を表す P。<br/>
    ///    次に数字＋年月日の間隔指定子を記述。 年月日を指定しない場合は、省略。<br/>
    ///    年月日と時間の間にはTを記述。 ※Tは、時間コンポーネントの先行時間指定子。<br/>
    ///    次に数字＋時分秒の間隔指定子を記述。<br/>
    /// </summary>
    /// <param name="timespanString">変換元の時刻文字列</param>
    /// <returns>変換後の TimeSpan</returns>
    public static TimeSpan ToTimeSpanFromIsoDurationString(string timespanString)
        => XmlConvert.ToTimeSpan(timespanString);


    /// <summary>
    /// 文字列を TimeSpan に変換する。<br/>
    ///    1 number  => d | d.h <br/>                       "0" to "1.00:00:00" | "1.2"  to "1.02:00:00"<br/>
    ///    2 numbers => h:m | d.h:m <br/>                   "2:3" to "02:03:00" | "1.2:3" to "1.02:03:00"<br/>
    ///    3 numbers => h:m:s | h:m:.f | h:m:s.f | d.h:m:s | d.h:m:.f |d.h:m:s.f <br/>      "2:3:4" to "02:03:04" | "2:3:.9" to "02:03:00.9" | "2:3:4.9" to "02:03:04.9"
    ///               "1.2:3:4" to "1.02:03:04" | "1.2:3:.9" to "1.02:03:00.9" | "1.2:3:4.9" to "1.02.03:04.9"
    /// </summary>
    /// <param name="timespanString">変換元の時刻文字列</param>
    /// <returns>変換後の TimeSpan</returns>
    /// <exception cref="FormatException">書式エラー</exception>
    public static TimeSpan ToTimeSpanFromTimeSpanString(string timespanString)
    {
        if (string.IsNullOrWhiteSpace(timespanString))
            return TimeSpan.Zero;

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
    /// ISO8601拡張 の日付文字列を DateOnly に変換する
    /// </summary>
    /// <param name="dateString">変換元の日時文字列</param>
    /// <returns>変換後の日付</returns>
    public static DateOnly ToDateOnly(this string dateString)
        => DateOnly.FromDateTime(dateString.ToDateTime());


    /// <summary>
    /// ISO8601拡張 の時刻文字列を TimeOnly に変換する
    /// </summary>
    /// <param name="timeString">変換元の時刻文字列</param>
    /// <returns>変換後の時刻</returns>
    public static TimeOnly ToTimeOnly(this string timeString)
        => TimeOnly.FromDateTime(timeString.ToDateTime());


    /// <summary>
    /// 日時をISO8601拡張文字列に変換する
    /// </summary>
    /// <param name="datetime">変換元の日時</param>
    /// <returns>変換後の日時</returns>
    public static string ToIsoString(this DateTime datetime)
        => new DateTime(datetime.Ticks, datetime.Kind == DateTimeKind.Unspecified ? DateTimeKind.Local : datetime.Kind)
            .ToString("yyyy-MM-ddThh:mm:ss.FFFFFFFzzz");


    /// <summary>
    /// 日付をISO8601拡張文字列に変換する
    /// </summary>
    /// <param name="date">変換元の日付</param>
    /// <param name="withOffset">TimeZoneOffsetを付加するか</param>
    /// <returns>変換後の日付</returns>
    public static string ToIsoString(this DateOnly date, bool withOffset = false)
        => date.ToString("yyyy-MM-dd") + (withOffset ? new DateTime(0, DateTimeKind.Local).ToString("zzz") : "");


    /// <summary>
    /// 時刻をISO8601拡張文字列に変換する
    /// </summary>
    /// <param name="time">変換元の時刻</param>
    /// <param name="withOffset">TimeZoneOffsetを付加するか</param>
    /// <returns>変換後の時刻</returns>
    public static string ToIsoString(this TimeOnly time, bool withOffset = false)
        => time.ToString("hh:mm:ss.FFFFFFF") + (withOffset ? new DateTime(0, DateTimeKind.Local).ToString("zzz") : "");


    /// <summary>
    /// TimeSpan を TimeSpan 文字列に変換する
    /// </summary>
    /// <param name="timespan">変換元の TimeSpan</param>
    /// <param name="enableDay">日の出力方法： true: 日付を含む d.hh 形式で出力、 false: 時間の積算形式 [hh] で出力</param>
    /// <returns>変換後の TimeSpan文字列</returns>
    public static string ToHmsString(this TimeSpan timespan, bool enableDay = true)
        => enableDay
            ? $"{timespan.Ticks:'';'-';''}{timespan.Days:0'.';0'.';''}{timespan:hh':'mm':'ss'.'FFFFFFF}".TrimEnd('.')
            : $"{(int)timespan.TotalHours:00}{timespan:':'mm':'ss'.'FFFFFFF}".TrimEnd('.');


    /// <summary>
    /// TimeSpan を ISO8601 Duration 文字列に変換する
    /// </summary>
    /// <param name="timespan">変換元の TimeSpan</param>
    /// <returns>変換後の Duration 文字列</returns>
    public static string ToIsoString(this TimeSpan timespan)
    {
        return XmlConvert.ToString(timespan);
    }
}

