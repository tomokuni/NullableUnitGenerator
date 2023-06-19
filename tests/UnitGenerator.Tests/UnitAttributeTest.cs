using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

using UnitGenerator.Tests;
using UGO = NullableUnitGenerator.UnitGenOpts;

namespace NullableUnitGenerator.Tests;


public static class ValidateExt
{
    public static IEnumerable<string> Validate<T>(this T obj) where T : class
    {
        var msg = new List<string>();
        var props = typeof(T).GetProperties();
        foreach (var prop in props)
        {
            foreach (var attr in prop.GetCustomAttributes())
            {
                if(attr is ValidationAttribute at && !at.IsValid(obj))
                    msg.Add($"{prop.Name}:error");
            }
        }

        return msg;
    }

}

public class UnitOfAttributeTest
{

    ///// <summary>IsValidForAttribute</summary>
    //private void ValidationWithUnitOfValidateAttribute<T>(T obj)
    //{
    //    var msg = new List<string>();
    //
    //    var vaa = typeof(VoBool).GetCustomAttribute<ValidationAttribute>();
    //    var va = typeof(T).GetCustomAttributes<ValidationAttribute>();
    //    foreach (var a in va)
    //    {
    //        if (!a.IsValid(obj))
    //        {
    //            var context = new ValidationContext(obj, null, null);
    //            var result = a.GetValidationResult(obj, context);
    //            if (result!.ErrorMessage is not null)
    //                msg.Add(result.ErrorMessage);
    //        }
    //    }
    //}

    //private static void Validate(object obj)
    //{
    //    foreach (PropertyInfo prop in obj.GetType().GetProperties())
    //    {
    //        // 値
    //        string? val = prop.GetValue(obj)?.ToString();
    //        // DisplayName属性取得
    //        DisplayNameAttribute? dispNameAttr = Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
    //        var name = dispNameAttr?.DisplayName ?? prop.Name;
    //        // Range属性取得
    //        RangeAttribute? rangeAttr = Attribute.GetCustomAttribute(prop, typeof(RangeAttribute)) as RangeAttribute;
    //        // StringLength属性取得
    //        StringLengthAttribute? lenAttr = Attribute.GetCustomAttribute(prop, typeof(StringLengthAttribute)) as StringLengthAttribute;
    //        // Required属性取得
    //        RequiredAttribute? reqAttr = Attribute.GetCustomAttribute(prop, typeof(RequiredAttribute)) as RequiredAttribute;
    //
    //        // チェック処理
    //        if (rangeAttr != null && !rangeAttr.IsValid(val))
    //        {
    //            Console.WriteLine(string.Format("{0}({1})の有効範囲は{2}～{3}です。", name, val, rangeAttr.Minimum, rangeAttr.Maximum));
    //        }
    //        if (lenAttr != null && !lenAttr.IsValid(val))
    //        {
    //            Console.WriteLine(string.Format("{0}({1})は最大桁数{2}桁です。", name, val, lenAttr.MaximumLength));
    //        }
    //        if (reqAttr != null && !reqAttr.IsValid(val))
    //        {
    //            Console.WriteLine(string.Format("{0}は必須項目です。", name));
    //        }
    //    }
    //}

    [Fact]
    public void Range_int()
    {
        var model = new Entity()
        {
            Id = new VoIntRange(2),
            Id2 = new VoIntRange(3),
            Id3 = new VoIntRange(4), 
        };
        var results = new List<ValidationResult>();
        ValidationContext context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        Assert.True(results.Any());
    }

    public class Entity
    {
        [DisplayName("ID")]
        //[UnitOfRange(1, 2)]
        //[UnitOfDefinedValidate]
        public VoIntRange Id { get; set; }

        [DisplayName("ID2")]
        //[UnitOfRange(1, 2)]
        //[UnitOfDefinedValidate]
        public VoIntRange Id2 { get; set; }

        [DisplayName("ID3")]
        //[UnitOfRange(1, 2)]
        //[UnitOfDefinedValidate]
        public VoIntRange Id3 { get; set; }
    }

}


[UnitOf(typeof(int), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfRange(4, 5)]
public readonly partial struct VoIntRange { }

