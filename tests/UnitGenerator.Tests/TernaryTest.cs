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
    TernaryStruct<int> viu1 = TernaryStruct<int>.UndefValue;
    TernaryStruct<int> viu2 = default;
    TernaryStruct<int> viu3 = new(TernaryState.Undef);
    TernaryStruct<int> vin1 = TernaryStruct<int>.NullValue;
    TernaryStruct<int> vin2 = new(null!);
    TernaryStruct<int> vin3 = new(TernaryState.Null);
    TernaryStruct<int> vid1 = TernaryStruct<int>.DefaultValue;
    TernaryStruct<int> vid2 = new(0);
    TernaryStruct<int> vid3 = new(TernaryState.Value);
    TernaryStruct<int> vi11 = new(1);
    TernaryStruct<int> vi12 = new(TernaryState.Value, 1);


    [Fact]
    public void StaticValue()
    {
        //
        // static Undef, Null, Default, GetBaseType
        //
        Assert.Equal(TernaryStruct<int>.UndefValue, viu1);
        Assert.Equal(TernaryStruct<int>.NullValue, vin1);
        Assert.Equal(TernaryStruct<int>.DefaultValue, vid1);
        Assert.True(TernaryStruct<int>.GetBaseType() == typeof(int));
    }


    [Fact]
    public void State()
    {
        //
        // Undef, Null, Default, Value
        //
        Assert.True(viu1.IsUndef);
        Assert.True(vin1.IsNull);
        Assert.True(vid1.IsDefault);
        Assert.True(vid1.HasValue);
        Assert.True(vi11.HasValue);

        Assert.True(viu1.State == TernaryState.Undef);
        Assert.True(vin1.State == TernaryState.Null);
        Assert.True(vid1.State == TernaryState.Value);
        Assert.True(vi11.State == TernaryState.Value);
    }


    [Fact]
    public void Value()
    {
        //
        // Undef, Null, Default, Value
        //
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.Value).Message);
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.GetOrThrow()).Message);
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.GetOrDefault()).Message);
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.GetOr(1)).Message);
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.GetOr(null)).Message);
        Assert.Equal(0, viu1.GetOrDefault(true));
        Assert.Equal(1, viu1.GetOr(1, true));
        Assert.Null(viu1.GetOr(null, true));
        Assert.False(viu1.TryGet(out var outViu1));  Assert.Equal(0, outViu1);

        Assert.Equal("~null~", Assert.Throws<InvalidOperationException>(() => vin1.Value).Message);
        Assert.Equal("~null~", Assert.Throws<InvalidOperationException>(() => vin1.GetOrThrow()).Message);
        Assert.Equal(0, vin1.GetOrDefault());
        Assert.Equal(0, vin1.GetOrDefault(true));
        Assert.Equal(1, vin1.GetOr(1));
        Assert.Equal(1, vin1.GetOr(1, true));
        Assert.Null(vin1.GetOr(null));
        Assert.Null(vin1.GetOr(null, true));
        Assert.False(vin1.TryGet(out var outVin1)); Assert.Equal(0, outVin1);

        Assert.Equal(0, vid1.Value);
        Assert.Equal(0, vid1.GetOrThrow());
        Assert.Equal(0, vid1.GetOrDefault());
        Assert.Equal(0, vid1.GetOrDefault(true));
        Assert.Equal(0, vid1.GetOr(1));
        Assert.Equal(0, vid1.GetOr(1, true));
        Assert.Equal(0, vid1.GetOr(null));
        Assert.Equal(0, vid1.GetOr(null, true));
        Assert.True(vid1.TryGet(out var outVid1)); Assert.Equal(0, outVid1);

        Assert.Equal(1, vi11.Value);
        Assert.Equal(1, vi11.GetOrThrow());
        Assert.Equal(1, vi11.GetOrDefault());
        Assert.Equal(1, vi11.GetOrDefault(true));
        Assert.Equal(1, vi11.GetOr(1));
        Assert.Equal(1, vi11.GetOr(1, true));
        Assert.Equal(1, vi11.GetOr(null));
        Assert.Equal(1, vi11.GetOr(null, true));
        Assert.True(vi11.TryGet(out var outVi11)); Assert.Equal(1, outVi11);
    }


    [Fact]
    public void Equal()
    {
        //
        // Equals, ==, !=
        //
        var ViuVals = new TernaryStruct<int>[] { viu1, viu2, viu3, vin1, vin2, vin3, vid1, vid2, vid3, vi11, vi12 };
        foreach (var (v1, i1) in ViuVals.Select((value, index) => (value, index)))
        { 
            foreach (var (v2, i2) in ViuVals.Select((value, index) => (value, index)))
            {
                if (v1.State == v2.State && v1.GetOrDefault(true) == v2.GetOrDefault(true))
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


public class ITernaryType
{
    ITernaryType<int> viu1 = TernaryStruct<int>.UndefValue;
    ITernaryType<int> viu2 = new TernaryStruct<int>();
    ITernaryType<int> viu3 = new TernaryStruct<int>(TernaryState.Undef);
    ITernaryType<int> vin1 = TernaryStruct<int>.NullValue;
    ITernaryType<int> vin2 = new TernaryStruct<int>((int?)null);
    ITernaryType<int> vin3 = new TernaryStruct<int>(TernaryState.Null);
    ITernaryType<int> vid1 = TernaryStruct<int>.DefaultValue;
    ITernaryType<int> vid2 = new TernaryStruct<int>(0);
    ITernaryType<int> vid3 = new TernaryStruct<int>(TernaryState.Value);
    ITernaryType<int> vi11 = new TernaryStruct<int>(1);
    ITernaryType<int> vi12 = new TernaryStruct<int>(TernaryState.Value, 1);


    [Fact]
    public void StaticValue()
    {
        //
        // static Undef, Null, Default, GetBaseType
        //
        Assert.Equal(TernaryStruct<int>.UndefValue, viu1);
        Assert.Equal(TernaryStruct<int>.NullValue, vin1);
        Assert.Equal(TernaryStruct<int>.DefaultValue, vid1);
        Assert.True(TernaryStruct<int>.GetBaseType() == typeof(int));
    }


    [Fact]
    public void State()
    {
        //
        // Undef, Null, Default, Value
        //
        Assert.True(viu1.IsUndef);
        Assert.True(vin1.IsNull);
        Assert.True(vid1.IsDefault);
        Assert.True(vid1.HasValue);
        Assert.True(vi11.HasValue);

        Assert.True(viu1.State == TernaryState.Undef);
        Assert.True(vin1.State == TernaryState.Null);
        Assert.True(vid1.State == TernaryState.Value);
        Assert.True(vi11.State == TernaryState.Value);
    }


    [Fact]
    public void Value()
    {
        //
        // Undef, Null, Default, Value
        //
        Assert.Equal("~undef~", Assert.Throws<InvalidOperationException>(() => viu1.Value).Message);

        Assert.Equal("~null~", Assert.Throws<InvalidOperationException>(() => vin1.Value).Message);

        Assert.Equal(0, vid1.Value);

        Assert.Equal(1, vi11.Value);
    }


    [Fact]
    public void Equal()
    {
        //
        // Equals, ==, !=
        //
        var ViuVals = new ITernaryType<int>[] { viu1, viu2, viu3, vin1, vin2, vin3, vid1, vid2, vid3, vi11, vi12 };
        foreach (var (v1, i1) in ViuVals.Select((value, index) => (value, index)))
        {
            foreach (var (v2, i2) in ViuVals.Select((value, index) => (value, index)))
            {
                if (v1.State == v2.State && v1.GetRawValue() == v2.GetRawValue())
                {
                    Assert.Equal(v1, v2);
                    Assert.True(v1.Equals(v2));
                    Assert.True(v1 == v2);
                    Assert.False(v1 != v2);

                    Assert.Equal(v1, (object?)v2);
                    Assert.True(v1.Equals((object?)v2));

                    if (v2.HasValue)
                    {
                        Assert.True(v1.Equals(v2.Value));
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

                    if (v2.HasValue)
                    {
                        Assert.False(v1.Equals(v2.Value));
                    }
                }
            }
        }
    }

}
