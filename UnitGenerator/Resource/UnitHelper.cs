#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
    /// Undef値以外のプロパティを取得して Dictionary に変換
    /// </summary>
    /// <param name="modelClass"></param>
    /// <returns></returns>
    public static IDictionary<string, dynamic> ExcludeUndefDictionary(object modelClass)
    {
        // プロパティ一覧を取得
        var properties = modelClass.GetType().GetProperties();

        // Undef値以外のプロパティ取得して Dictionary に変換
        var dic = properties
            .Select(s => new { p = s, v = s.GetValue(modelClass)! })
            .Where(w => w.v is not null && (!(w.v as IUnitOf)?.IsUndef ?? false))
            .ToDictionary(x => x.p.Name, x => (dynamic)x.v);
        return dic;
    }

}
