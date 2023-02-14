using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NullableUnitGenerator;


/// <summary>
/// UnitOfHelper
/// </summary>
public static class UnitOfHelper
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

}
