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
/// UnitOf のヘルパー関数群
/// </summary>
public static class UnitHelper
{
    /// <summary>
    /// &lt;T&gt; Get the class type and attribute information to which the attribute is assigned.<br/>
    /// &lt;T&gt;属性が付与されたクラス型と属性情報を取得する。<br/>
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<(Type type, T attr)> GetTypeAndAttributes<T>() where T : Attribute
    {
        var asms = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic);
        var types = asms.SelectMany(asm => asm.GetExportedTypes());
        var typesHasAttr = types.Where(w => w.GetCustomAttributes(typeof(T), false).Any());
        var attrs = typesHasAttr.Select(s => (s.UnderlyingSystemType, (T)s.GetCustomAttributes(typeof(T), false).First()));
        return attrs.ToList();
    }


    /// <summary>
    /// Convert to object (dynamic as ExpandoObject) that does not contain Undef value properties.<br/>
    /// Undef値のプロパティを含まないオブジェクト（dynamic as ExpandoObject）に変換する。<br/>
    /// </summary>
    /// <param name="modelClass">Source Objects</param>
    /// <returns>Objects that do not contain Undef value properties<br/>Undef値のプロパティを含まないオブジェクト</returns>
    public static dynamic ExcludeUndef(object modelClass)
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

}
