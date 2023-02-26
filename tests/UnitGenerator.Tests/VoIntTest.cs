using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Xunit;
using NullableUnitGenerator;

namespace NullableUnitGenerator.Tests;

public class VoIntTest
{
    [Fact]
    public void Equal()
    {
        VoInt a = default;
        VoInt b = new(null);
        VoInt c = new(default(int));
        VoInt d = new(0);
        VoInt e = new(1);

        //
        // Undefined, Null, Default
        //
        Assert.Equal(a, VoInt.UndefValue);
        Assert.Equal(b, VoInt.NullValue);
        Assert.Equal(c, VoInt.ValueStateDefaultValue);

        //
        // Equals, ==, !=
        //
        var valsVoInt = new VoInt[] { a, b, c, d, e };
        foreach (var (v1, i1) in valsVoInt.Select((value, index) => (value, index)))
        {
            foreach (var (v2, i2) in valsVoInt.Select((value, index) => (value, index)))
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

    [Fact]
    public void Compare()
    {
        VoInt a = default;
        VoInt b = new(null);
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
}


[UnitOf(typeof(int), UnitGenerateOptions.ParseMethod | UnitGenerateOptions.MinMaxMethod | UnitGenerateOptions.ArithmeticOperator | UnitGenerateOptions.ValueArithmeticOperator | UnitGenerateOptions.Comparable | UnitGenerateOptions.Validate | UnitGenerateOptions.JsonConverter | UnitGenerateOptions.MessagePackFormatter | UnitGenerateOptions.DapperTypeHandler | UnitGenerateOptions.EntityFrameworkValueConverter | UnitGenerateOptions.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoInt
{
    private partial void Validate()
    {
        _ = HasValue;
    }
}
