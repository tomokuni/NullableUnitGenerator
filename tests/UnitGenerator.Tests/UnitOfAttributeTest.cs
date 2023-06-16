using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

using UGO = NullableUnitGenerator.UnitGenOpts;
using System.Reflection;
using System;
using Newtonsoft.Json;
using System.Text;

namespace NullableUnitGenerator.Tests;


public static class ValidateExt
{
    public static string Validate<T>(this T obj) where T : class
    {
        var msg = new StringBuilder();
        var props = typeof(T).GetProperties();
        foreach (var prop in props)
        {
            foreach (var attr in prop.GetCustomAttributes())
            {
                if(attr is ValidationAttribute at && !at.IsValid(obj))
                    msg.AppendLine($"{prop.Name}:error");
            }
        }

        return msg.ToString();
    }
}

public class UnitOfAttributeTest
{
    private static void Validate(object obj)
    {
        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        {
            // 値
            string? val = prop.GetValue(obj)?.ToString();
            // DisplayName属性取得
            DisplayNameAttribute? dispNameAttr = Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            var name = dispNameAttr?.DisplayName ?? prop.Name;
            // Range属性取得
            RangeAttribute? rangeAttr = Attribute.GetCustomAttribute(prop, typeof(RangeAttribute)) as RangeAttribute;
            // StringLength属性取得
            StringLengthAttribute? lenAttr = Attribute.GetCustomAttribute(prop, typeof(StringLengthAttribute)) as StringLengthAttribute;
            // Required属性取得
            RequiredAttribute? reqAttr = Attribute.GetCustomAttribute(prop, typeof(RequiredAttribute)) as RequiredAttribute;

            // チェック処理
            if (rangeAttr != null && !rangeAttr.IsValid(val))
            {
                Console.WriteLine(string.Format("{0}({1})の有効範囲は{2}～{3}です。", name, val, rangeAttr.Minimum, rangeAttr.Maximum));
            }
            if (lenAttr != null && !lenAttr.IsValid(val))
            {
                Console.WriteLine(string.Format("{0}({1})は最大桁数{2}桁です。", name, val, lenAttr.MaximumLength));
            }
            if (reqAttr != null && !reqAttr.IsValid(val))
            {
                Console.WriteLine(string.Format("{0}は必須項目です。", name));
            }
        }
    }

    [Fact]
    public void Range_int()
    {
        var a = new VoIntRange(4);
        Validate(a);

        var b = new Entity()
        {
            Id = new VoIntRange(4),
        };
        Validate(b);
        b.Validate();
    }

    public class Entity
    {
        [DisplayName("ID")]
        [UnitOfRange(1, 2)]
        public VoIntRange Id { get; set; }
    }

}


[UnitOf(typeof(int), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
public readonly partial struct VoIntRange { }

