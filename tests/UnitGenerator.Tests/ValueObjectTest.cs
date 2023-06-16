using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Xunit;
using NullableUnitGenerator;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Xunit.Sdk;
using static NullableUnitGenerator.Tests.ValueObjectTest;
using UnitGenerator.Tests;

namespace NullableUnitGenerator.Tests;


public class ValueObjectTest
{
    [Fact]
    public void VoBool_Equal()
    {
        var vo = new VoBool[] { default, new((bool?)null), new(default(bool)), new(false), new(true) };

        //
        // Undefined, Null, Default
        //
        Assert.Equal(vo[0], VoBool.UndefValue);
        Assert.Equal(vo[1], VoBool.NullValue);
        Assert.Equal(vo[2], VoBool.DefaultValueOfValueState);

        //
        // Equals, ==, !=
        //
        foreach (var (v1, i1) in vo.Select((value, index) => (value, index)))
        {
            foreach (var (v2, i2) in vo.Select((value, index) => (value, index)))
            {
                if (v1.HasValue
                    && v2.HasValue
                    && v1.Value.Equals(v2.Value))
                {
                    Assert.True(v1.Equals(v2));
                    Assert.True(v1.Equals((object)v2));
                    Assert.True(v1 == v2);
                    Assert.False(v1 != v2);
                    if (v2.HasValue)
                    {
                        //Assert.True(v1.Equals(v2.Value));
                        //Assert.True(v1 == v2.Value);
                        //Assert.False(v1 != v2.Value);
                    }
                    else
                    {
                        //Assert.False(v1.Equals(v2.Value));
                        //Assert.False(v1 == v2.Value);
                        //Assert.False(v1 != v2.Value);
                    }
                }
                else
                {
                    if (v1.IsUndef.Equals(v2.IsUndef) && v1.IsUndef)
                    {
                        Assert.True(v1.Equals(v2));
                        Assert.True(v1.Equals((object)v2));
                        Assert.True(v1 == v2);
                        Assert.False(v1 != v2);
                    }
                    else if (v1.IsNull.Equals(v2.IsNull) && v1.IsNull)
                    {
                        Assert.True(v1.Equals(v2));
                        Assert.True(v1.Equals((object)v2));
                        Assert.True(v1 == v2);
                        Assert.False(v1 != v2);
                    }
                    else
                    {
                        Assert.False(v1.Equals(v2));
                        Assert.False(v1.Equals((object)v2));
                        Assert.False(v1 == v2);
                        Assert.True(v1 != v2);
                    }
                }
            }
        }
    }



    //var tw = new TestVoWrapper<VoBool, bool>();
    //tw.Equal(vo);


    //public void Equal()
    //{
    //    VoInt a = default;
    //    VoInt b = new((int?)null);
    //    VoInt c = new(default(int));
    //    VoInt d = new(0);
    //    VoInt e = new(1);

    //    //
    //    // Undefined, Null, Default
    //    //
    //    Assert.Equal(a, VoInt.UndefValue);
    //    Assert.Equal(b, VoInt.NullValue);
    //    Assert.Equal(c, VoInt.DefaultValueOfValueState);

    //    //
    //    // Equals, ==, !=
    //    //
    //    var valsVoInt = new VoInt[] { a, b, c, d, e };
    //    foreach (var (v1, i1) in valsVoInt.Select((value, index) => (value, index)))
    //    {
    //        foreach (var (v2, i2) in valsVoInt.Select((value, index) => (value, index)))
    //        {
    //            if (v1.HasValue 
    //                && v2.HasValue
    //                && v1.Value.Equals(v2.Value))
    //            {
    //                Assert.True(v1.Equals(v2));
    //                Assert.True(v1.Equals((object)v2));
    //                Assert.True(v1 == v2);
    //                Assert.False(v1 != v2);
    //                if (v2.HasValue)
    //                {
    //                    //Assert.True(v1.Equals(v2.Value));
    //                    //Assert.True(v1 == v2.Value);
    //                    //Assert.False(v1 != v2.Value);
    //                }
    //                else
    //                {
    //                    //Assert.False(v1.Equals(v2.Value));
    //                    //Assert.False(v1 == v2.Value);
    //                    //Assert.False(v1 != v2.Value);
    //                }
    //            }
    //            else
    //            {
    //                if (v1.IsUndef.Equals(v2.IsUndef) && v1.IsUndef)
    //                {
    //                    Assert.True(v1.Equals(v2));
    //                    Assert.True(v1.Equals((object)v2));
    //                    Assert.True(v1 == v2);
    //                    Assert.False(v1 != v2);
    //                }
    //                else if (v1.IsNull.Equals(v2.IsNull) && v1.IsNull)
    //                {
    //                    Assert.True(v1.Equals(v2));
    //                    Assert.True(v1.Equals((object)v2));
    //                    Assert.True(v1 == v2);
    //                    Assert.False(v1 != v2);
    //                }
    //                else
    //                {
    //                    Assert.False(v1.Equals(v2));
    //                    Assert.False(v1.Equals((object)v2));
    //                    Assert.False(v1 == v2);
    //                    Assert.True(v1 != v2);
    //                }
    //            }
    //        }
    //    }
    //}

    [Fact]
    public void Compare()
    {
        VoInt a = default;
        VoInt b = new((int?)null);
        VoInt c = new(default(int));
        VoInt d = new(0);
        VoInt e = new(1);

        //
        // Compare, >, <, >=, <=
        //
        var valsVoInt = new VoInt[] { a, b, c, d, e };
        foreach (var (v1, i1) in valsVoInt.Select((value, index) => (value, index)))
        {
            foreach (var (v2, i2) in valsVoInt.Select((value, index) => (value, index)))
            {
                if (v1.HasValue
                    && v2.HasValue)
                {
                    if (v1.Value == v2.Value)
                    {
                        Assert.True(v1 >= v2);
                        //Assert.True(v1 >= v2.Value);
                        Assert.True(v1 <= v2);
                        //Assert.True(v1 <= v2.Value);
                    }
                    if (v1.Value > v2.Value)
                    {
                        Assert.True(v1 > v2);
                        Assert.True(v1 >= v2);
                        //Assert.True(v1 > v2.Value);
                        //Assert.True(v1 >= v2.Value);
                        Assert.False(v1 < v2);
                        Assert.False(v1 <= v2);
                        //Assert.False(v1 < v2.Value);
                        //Assert.False(v1 <= v2.Value);
                    }
                    if (v1.Value >= v2.Value)
                    {
                        Assert.True(v1 >= v2);
                        //Assert.True(v1 >= v2.Value);
                    }
                    if (v1.Value < v2.Value)
                    {
                        Assert.True(v1 < v2);
                        Assert.True(v1 <= v2);
                        //Assert.True(v1 < v2.Value);
                        //Assert.True(v1 <= v2.Value);
                        Assert.False(v1 > v2);
                        Assert.False(v1 >= v2);
                        //Assert.False(v1 > v2.Value);
                        //Assert.False(v1 >= v2.Value);
                    }
                    if (v1.Value <= v2.Value)
                    {
                        Assert.True(v1 <= v2);
                        //Assert.True(v1 <= v2.Value);
                    }
                }
                else
                {
                    Assert.False(v1 > v2);
                    Assert.False(v1 >= v2);
                    Assert.False(v1 < v2);
                    Assert.False(v1 <= v2);
                }
            }
        }

    }

    [Fact]
    public void ExistMethod()
    {
        Type t = typeof(DateTime);
        //List<string> OverloadOperators = t.GetMethods().Where(x => x.Name.StartsWith("op_")).Select(x => x.Name).Distinct().OrderBy(x => x).ToList();
        //List<string> Operators = t.GetMethods().Select(x => x.Name).Distinct().OrderBy(x => x).ToList();
        //string OperatorsString = string.Join(", ", Operators);
        List<MethodInfo> Operators = t.GetMethods().ToList();

        var a = Type.GetType("Byte", false, true);
        var b = Type.GetType("byte", false, true);
        var c = Type.GetType("System.Byte[]", false, true);
        var d = Type.GetType("System.byte[]",false, true);

        //MethodInfo? mi = t.GetMethod(
        //    "op_Implicit",
        //    (BindingFlags.Public | BindingFlags.Static),
        //    null,
        //    new Type[] { (new object()).GetType() },
        //    new ParameterModifier[0]);
        //Debug.WriteLine($"{mi}");

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        var aaa = dt1 > dt2;
    }
}


[UnitOf(typeof(int), UnitGenOpts.ArithmeticOperator | UnitGenOpts.ValueArithmeticOperator | UnitGenOpts.ComparisonOperator | UnitGenOpts.IComparable | UnitGenOpts.ImplicitOperator | UnitGenOpts.ParseMethod | UnitGenOpts.MinMaxMethod | UnitGenOpts.ValidateAtGeneration | UnitGenOpts.JsonConverter | UnitGenOpts.MessagePackFormatter | UnitGenOpts.DapperTypeHandler | UnitGenOpts.EntityFrameworkValueConverter | UnitGenOpts.JsonConverterDictionaryKey)]
public readonly partial struct VoInt
{
    partial void CustomValidate()
    {
    }
}

//[UnitOf(typeof(DateTime), UnitGenOpts.ParseMethod | UnitGenOpts.MinMaxMethod | UnitGenOpts.ArithmeticOperator | UnitGenOpts.ValueArithmeticOperator | UnitGenOpts.Comparable | UnitGenOpts.Validate | UnitGenOpts.JsonConverter | UnitGenOpts.MessagePackFormatter | UnitGenOpts.DapperTypeHandler | UnitGenOpts.EntityFrameworkValueConverter | UnitGenOpts.JsonConverterDictionaryKey)]
//public readonly partial struct VoDateTime
//{
//    private partial void Validate()
//    {
//        _ = HasValue;
//    }
//}
