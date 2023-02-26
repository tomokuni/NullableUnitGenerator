using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Xunit;
using NullableUnitGenerator;
using Newtonsoft.Json.Linq;
using Estable.TernaryType;

namespace TernaryType.Tests;


public class TernaryStruct
{
    TernaryTypeS<int> viu1 = TernaryTypeS<int>.UndefValue;
    TernaryTypeS<int> viu2 = default;
    TernaryTypeS<int> viu3 = new(Estable.TernaryType.TernaryState.Undef);
    TernaryTypeS<int> vin1 = TernaryTypeS<int>.NullValue;
    TernaryTypeS<int> vin2 = new(null);
    TernaryTypeS<int> vin3 = new(Estable.TernaryType.TernaryState.Null);
    TernaryTypeS<int> vid1 = TernaryTypeS<int>.ValueStateDefaultValue;
    TernaryTypeS<int> vid2 = new(0);
    TernaryTypeS<int> vid3 = new(Estable.TernaryType.TernaryState.Value);
    TernaryTypeS<int> vi11 = new(1);
    TernaryTypeS<int> vi12 = new(Estable.TernaryType.TernaryState.Value, 1);


    [Fact]
    public void StaticValue()
    {
        //
        // static Undef, Null, Default, GetBaseType
        //
        Assert.Equal(TernaryTypeS<int>.UndefValue, viu1);
        Assert.Equal(TernaryTypeS<int>.NullValue, vin1);
        Assert.Equal(TernaryTypeS<int>.ValueStateDefaultValue, vid1);
        Assert.True(TernaryTypeS<int>.GetBaseType() == typeof(int));
    }


    [Fact]
    public void State()
    {
        //
        // Undef, Null, Default, Value
        //
        Assert.True(viu1.IsUndef);
        Assert.True(vin1.IsNull);
        Assert.True(vid1.HasValue);
        Assert.True(vi11.HasValue);

        Assert.True(viu1.State == Estable.TernaryType.TernaryState.Undef);
        Assert.True(vin1.State == Estable.TernaryType.TernaryState.Null);
        Assert.True(vid1.State == Estable.TernaryType.TernaryState.Value);
        Assert.True(vi11.State == Estable.TernaryType.TernaryState.Value);
    }


    [Fact]
    public void Value()
    {
        //
        // Undef, Null, Default, Value
        //
        var UndefMsg = $"Value is {NullableUnitGenerator.TernaryState.Undef}.";
        Assert.Equal(UndefMsg, Assert.Throws<InvalidOperationException>(() => viu1.Value).Message);
        Assert.Equal(UndefMsg, Assert.Throws<InvalidOperationException>(() => viu1.GetOrThrow()).Message);
        Assert.Equal(0, viu1.GetOrDefault());
        Assert.Equal(1, viu1.GetOr(1));
        Assert.Null(viu1.GetOr(null));
        Assert.False(viu1.TryGet(out var outViu1));  Assert.Equal(0, outViu1);

        var NullMsg = $"Value is {NullableUnitGenerator.TernaryState.Null}.";
        Assert.Equal(NullMsg, Assert.Throws<InvalidOperationException>(() => vin1.Value).Message);
        Assert.Equal(NullMsg, Assert.Throws<InvalidOperationException>(() => vin1.GetOrThrow()).Message);
        Assert.Equal(0, vin1.GetOrDefault());
        Assert.Equal(1, vin1.GetOr(1));
        Assert.Null(vin1.GetOr(null));
        Assert.False(vin1.TryGet(out var outVin1)); Assert.Equal(0, outVin1);

        Assert.Equal(0, vid1.Value);
        Assert.Equal(0, vid1.GetOrThrow());
        Assert.Equal(0, vid1.GetOrDefault());
        Assert.Equal(0, vid1.GetOr(1));
        Assert.Equal(0, vid1.GetOr(null));
        Assert.True(vid1.TryGet(out var outVid1)); Assert.Equal(0, outVid1);

        Assert.Equal(1, vi11.Value);
        Assert.Equal(1, vi11.GetOrThrow());
        Assert.Equal(1, vi11.GetOrDefault());
        Assert.Equal(1, vi11.GetOr(1));
        Assert.Equal(1, vi11.GetOr(null));
        Assert.True(vi11.TryGet(out var outVi11)); Assert.Equal(1, outVi11);
    }


    [Fact]
    public void Equal()
    {
        //
        // Equals, ==, !=
        //
        var ViuVals = new TernaryTypeS<int>[] { viu1, viu2, viu3, vin1, vin2, vin3, vid1, vid2, vid3, vi11, vi12 };
        foreach (var (v1, i1) in ViuVals.Select((value, index) => (value, index)))
        { 
            foreach (var (v2, i2) in ViuVals.Select((value, index) => (value, index)))
            {
                if (v1.State == v2.State && v1.GetOrDefault() == v2.GetOrDefault())
                {
                    Assert.Equal(v1, v2);
                    Assert.True(v1.Equals(v2));
                    Assert.True(v1 == v2);
                    Assert.False(v1 != v2);
                    
                    Assert.Equal(v1, (object?)v2);
                    Assert.True(v1.Equals((object?)v2));

                    if (!v2.IsUndef)
                    {
                        Assert.True(v1.Equals(v2.GetOr(null)));
                        Assert.True(v1 == v2.GetOr(null));
                        Assert.False(v1 != v2.GetOr(null));
                        Assert.True(v2.GetOr(null) == v1);
                        Assert.False(v2.GetOr(null) != v1);
                    }

                    if (v2.HasValue)
                    {
                        Assert.True(v1.Equals(v2.Value));
                        Assert.True(v1 == v2.Value);
                        Assert.False(v1 != v2.Value);
                        Assert.True(v2.Value == v1);
                        Assert.False(v2.Value != v1);
                    }
                }
                else
                {
                    Assert.NotEqual(v1, v2);
                    Assert.False(v1.Equals(v2));
                    Assert.False(v1 == v2);
                    Assert.True(v1 != v2);
                    Assert.NotEqual(v1, (object?)v2);
                    Assert.False(v1.Equals((object?)v2));

                    if (!v2.IsUndef)
                    {
                        Assert.False(v1.Equals(v2.GetOr(null)));
                        Assert.False(v1 == v2.GetOr(null));
                        Assert.True(v1 != v2.GetOr(null));
                        Assert.False(v2.GetOr(null) == v1);
                        Assert.True(v2.GetOr(null) != v1);
                    }

                    if (v2.HasValue)
                    {
                        Assert.False(v1.Equals(v2.Value));
                        Assert.False(v1 == v2.Value);
                        Assert.True(v1 != v2.Value);
                        Assert.False(v2.Value == v1);
                        Assert.True(v2.Value != v1);
                    }
                }
            }
        }
    }

}
