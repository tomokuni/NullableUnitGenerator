#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NullableUnitGenerator;


/// <summary>
/// UnitOf のヘルパー関数群
/// </summary>
public static class UnitHelper
{
    /// <summary>
    /// &lt;T&gt;属性が付与されたクラス型と属性情報を取得する
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<(Type type, T attr)> GetTypeAndAttributes<T>() where T : Attribute
    {
        var asms = AppDomain.CurrentDomain.GetAssemblies();
        var types = asms.SelectMany(asm => asm.GetExportedTypes());
        var typesHasAttr = types.Where(w => w.GetCustomAttributes(typeof(T), false).Any());
        var attrs = typesHasAttr.Select(s => (s.UnderlyingSystemType, (T)s.GetCustomAttributes(typeof(T), false).First()));
        return attrs.ToList();
    }


    /// <summary>
    /// 引数のオブジェクトからプロパティを取得し、Undef値以外を ExpandoObject 型に変換
    /// </summary>
    /// <param name="modelClass"></param>
    /// <returns></returns>
    public static ExpandoObject ExcludeUndef(object modelClass)
    {
        if (modelClass is IDictionary<string, dynamic> d)
        {
            var dic = d
                // Undef値以外のプロパティを取得して Dictionary に変換
                .Where(w => (w.Value is IUnitOf uo && !uo.IsUndef) || (w.Value is not IUnitOf))
                .ToDictionary(x => x.Key, x => (dynamic)x.Value!);
            var res = dic.Aggregate(new ExpandoObject() as IDictionary<string, dynamic>,
                (a, p) => { a.Add(p); return a; }) as ExpandoObject;
            return res!;
        }
        else
        {
            var dic = modelClass.GetType().GetProperties()
                // Undef値以外のプロパティを取得して Dictionary に変換
                .Where(w => (w.GetValue(modelClass) is IUnitOf uo && !uo.IsUndef) || (w.GetValue(modelClass) is not IUnitOf))
                .ToDictionary(x => x.Name, x => (dynamic)x.GetValue(modelClass)!);
            var res = dic.Aggregate(new ExpandoObject() as IDictionary<string, dynamic>,
                (a, p) => { a.Add(p); return a; }) as ExpandoObject;
            return res!;
        }
    }


    /// <summary>
    /// 文字列をキャメルケースに変換する
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToCamelCase(string str)
    {
        var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);

        var leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
            m =>  m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value);

        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            .ToArray();

        return $"{leadWord}{string.Join(string.Empty, tailWords)}";
    }


    /// <summary>
    /// 文字列をパスカルケースに変換する
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToPascalCase(string str)
    {
        var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);

        var leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
            m => m.Groups[1].Value.ToUpper() + m.Groups[2].Value.ToLower() + m.Groups[3].Value);

        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            .ToArray();

        return $"{leadWord}{string.Join(string.Empty, tailWords)}";
    }
}
