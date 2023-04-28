﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY NullableUnitGenerator. DO NOT CHANGE IT.
// </auto-generated>
#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
using System;
using System.Text.Json.Serialization;
using NullableUnitGenerator;

namespace WebApiApp.Models.Base;


/// <summary>
/// VoInt is Nullable ValueObject<br/>
/// Primitive type is int
/// </summary>
[JsonConverter(typeof(VoIntJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(VoIntTypeConverter))]
public readonly partial struct VoInt : IEquatable<VoInt>, IComparable<VoInt>
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public VoInt(VoInt value)
    {
        m_state = HAS_VALUE;
        m_value = value.m_value;

    }

    /// <summary>Complete Constructor</summary>
    public VoInt(int value)
    {
        m_state = HAS_VALUE;
        m_value = (int)value;

    }

    /// <summary>Complete Constructor</summary>
    public VoInt(int? value)
    {
        if (value is not null)
        {
            m_state = HAS_VALUE;
            m_value = (int)value;
        }
        else
        {
            m_state = NULL_VALUE;
            m_value = default(int);
        }

    }


    //
    // static property
    //

    /// <summary>undefined value instance.</summary>
    public static VoInt UndefinedValue
        => new();

    /// <summary>null value instance.</summary>
    public static VoInt NullValue
        => new((int?)null);

    /// <summary>default value instance.</summary>
    public static VoInt DefaultValue
        => new(default(int));

    /// <summary>default value instance.</summary>
    public static Type PrimitiveType
        => typeof(int);


    //
    // const
    //

    internal const byte UNDEFINED_VALUE = 0;
    internal const byte NULL_VALUE = 1;
    internal const byte HAS_VALUE = 3;


    //
    // backing field
    //

    [JsonPropertyName("value")]
    internal readonly int m_value = default(int);
    [JsonPropertyName("state")]
    internal readonly byte m_state = UNDEFINED_VALUE;


    //
    // get state property
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    [JsonIgnore]
    public bool IsUndefined
        => m_state == UNDEFINED_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    [JsonIgnore]
    public bool IsNull
        => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    [JsonIgnore]
    public bool HasValue
        => m_state == HAS_VALUE;

    public byte State
        => m_state;


    //
    // get value property
    //

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public int Value
        => HasValue ? m_value : throw new InvalidOperationException("NoValue");

    /// <inheritdoc cref="Value" />
    public int AsPrimitive()
        => Value;

    /// <summary>return value if HasValue is true; otherwise, <see langword="default(T)"/></summary>
    /// <returns><b>value</b> : if assigned and not null<br/><b><see langword="default(T)"/></b> : otherwise</returns>
    public int GetOrDefault()
        => HasValue ? m_value : default(int);

    /// <inheritdoc cref="OrDefault" />
    public int? GetOrDefault(int? defaultValue)
        => HasValue ? m_value : defaultValue;

    /// <summary>return value if HasValue is true; otherwise, null</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b><see langword="null"/></b> : otherwise</returns>
    public int? GetOrNull()
        => HasValue ? m_value : null;

    /// <inheritdoc cref="Value" />
    public int GetOrThrow()
        => Value;

    /// <summary>
    /// return true and out parameter value if HasValue is true; otherwise, false.
    /// </summary>
    /// <param name="value">value</param>
    /// <returns><b><see langword="true"/> and out parameter value</b> : if HasValue is true,</returns>
    public bool TryGet(out int value)
    {
        value = m_value;
        return HasValue;
    }


    //
    // GetHashCode
    //

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => new { IsUndefined, IsNull, m_value }.GetHashCode();


    //
    // ToString
    //

    /// <summary>Returns this instance of System.String; no actual conversion is performed.</summary>
    /// <returns>The current string.</returns>
    public override string ToString()
        => HasValue ? m_value.ToString() : $"<{(IsUndefined ? "undefined" : "null")}>";


    //
    // MaxValue, MinValue
    //

    /// <summary>Represents the largest possible value. This field is constant.</summary>
    public const int MaxValue = int.MaxValue;

    /// <summary>Represents the smallest possible value. This field is constant.</summary>
    public const int MinValue = int.MinValue;


    //
    // Equals, IEquatable<VoInt>
    //

    /// <summary>Returns a value indicating whether this instance is same value to a specified VoInt value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public bool Equals(VoInt other)
        => m_value.Equals(other.m_value) &&
            IsNull.Equals(other.IsNull) &&
            IsUndefined.Equals(other.IsUndefined);

    /// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    /// <returns>true if obj is an instance of int or VoInt and equals the value of this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => (IsNull && obj is null) ||
        obj switch
        {
            VoInt vo => Equals(vo),
            int v => Equals(new(v)),
            _ => false,
        };


    //
    // CompareTo, IComparable<VoInt>    // UGO.Comparable
    //

    /// <summary>Compares this instance to a specified VoInt and returns an indication of their relative values.</summary>
    /// <returns>
    /// A signed number indicating the relative values of this instance and value.<br/>
    /// <b>Return Value</b> – Description<br/>
    /// <b>Less than zero</b> – This instance is less than value.<br/>
    /// <b>Zero</b> – This instance is equal to value.<br/>
    /// <b>Greater than zero</b> – This instance is greater than value.
    /// </returns>
    public int CompareTo(VoInt other)
        => IsUndefinedOrNull(this, other, out VoInt result)
            ? 0
            : m_value.CompareTo(other.m_value);


    //
    // IsUndefinedOrNull
    //

    /// <summary>Determine if a value is undefined or null.</summary>
    /// <returns>
    /// <b>Return Value</b> – <b>out result Value</b> – Description<br/>
    /// <b>true</b> – <b>UndefinedValue</b> – if value is undefined.<br/>
    /// <b>true</b> – <b>NullValue</b> – if value is null.<br/>
    /// <b>false</b> – <b>DefaultValue</b> – if value is set.
    /// </returns>
    public static bool IsUndefinedOrNull(in VoInt x, out VoInt result)
    {
        if (x.IsUndefined)
        {
            result = UndefinedValue;
            return true;
        }

        if (x.IsNull)
        {
            result = NullValue;
            return true;
        }

        result = DefaultValue;
        return false;
    }

    /// <summary>DDetermines if either value is undefined or null.</summary>
    /// <returns>
    /// <b>Return Value</b> – <b>out result Value</b> – Description<br/>
    /// <b>true</b> – <b>UndefinedValue</b> – if either value is undefined.<br/>
    /// <b>true</b> – <b>NullValue</b> – if either value is null.<br/>
    /// <b>false</b> – <b>DefaultValue</b> – if either value is set.
    /// </returns>
    public static bool IsUndefinedOrNull(in VoInt x, in VoInt y, out VoInt result)
    {
        if (x.IsUndefined || y.IsUndefined)
        {
            result = UndefinedValue;
            return true;
        }

        if (x.IsNull || y.IsNull)
        {
            result = NullValue;
            return true;
        }

        result = DefaultValue;
        return false;
    }


    //
    // Parse, TryParse    // UGO.ParseMethod
    //

    /// <summary>Converts the string representation of a number.</summary>
    /// <returns>A equivalent to the number contained in s.</returns>
    /// <exception cref="System.ArgumentNullException" >s is null.</exception>
    /// <exception cref="System.FormatException" >is not in the correct format.</exception>
    /// <exception cref="System.OverflowException" >s represents a number less than MinValue or greater than MaxValue.</exception>
    public static VoInt Parse(string s)
        => new VoInt(int.Parse(s));

    /// <summary>Converts the string representation of a number. A return value indicates whether the conversion succeeded.</summary>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string s, out VoInt result)
    {
        if (int.TryParse(s, out var r))
        {
            result = new VoInt(r);
            return true;
        }
        else
        {
            result = NullValue;
            return false;
        }
    }


    //
    // implicit, explicit operator
    //

    /// <summary>explicit operator</summary>
    /// <returns>int? value.</returns>
    public static explicit operator int?(VoInt value)
        => (int?)value.GetOrNull();

    /// <summary>explicit operator</summary>
    /// <returns>VoInt value.</returns>
    public static explicit operator VoInt(int? value)
        => new VoInt(value);


    //
    // ==, != operator
    //

    /// <summary>Returns a value indicating whether two instances are same value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator ==(in VoInt x, in VoInt y)
        => x.Equals(y);

    /// <summary>Returns a value indicates whether two instances are different values.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator !=(in VoInt x, in VoInt y)
        => !x.Equals(y);

    /// <summary>Returns a value indicating whether two instances are same value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator ==(in VoInt x, in int? y)
        => x.Equals(y);

    /// <summary>Returns a value indicates whether two instances are different values.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator !=(in VoInt x, in int? y)
        => !x.Equals(y);


    //
    // >, <, >=, <= operator    // UGO.Comparable and WithoutComparisonOperator
    //

    /// <summary>operator &gt;</summary>
    public static bool operator >(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? false
            : x.m_value > y.m_value;

    /// <summary>operator &lt;</summary>
    public static bool operator <(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? false
            : x.m_value < y.m_value;

    /// <summary>operator &gt;=</summary>
    public static bool operator >=(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? false
            : x.m_value >= y.m_value;

    /// <summary>operator &lt;=</summary>
    public static bool operator <=(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? false
            : x.m_value <= y.m_value;

    /// <summary>operator &gt;</summary>
    public static bool operator >(in VoInt x, in int? y)
        => IsUndefinedOrNull(x, new VoInt(y), out VoInt result)
            ? false
            : x.m_value > y;

    /// <summary>operator &lt;</summary>
    public static bool operator <(in VoInt x, in int? y)
        => IsUndefinedOrNull(x, new VoInt(y), out VoInt result)
            ? false
            : x.m_value < y;

    /// <summary>operator &gt;=</summary>
    public static bool operator >=(in VoInt x, in int? y)
        => IsUndefinedOrNull(x, new VoInt(y), out VoInt result)
            ? false
            : x.m_value >= y;

    /// <summary>operator &lt;=</summary>
    public static bool operator <=(in VoInt x, in int? y)
        => IsUndefinedOrNull(x, new VoInt(y), out VoInt result)
            ? false
            : x.m_value <= y;


    //
    // UGO.MinMaxMethod
    //

    /// <summary>Min</summary>
    public static VoInt Min(VoInt x, VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(Math.Min(x.m_value, y.m_value));

    /// <summary>Max</summary>
    public static VoInt Max(VoInt x, VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(Math.Max(x.m_value, y.m_value));


    //
    // +, -, *, /, % operator    UGO.ArithmeticOperator
    //

    /// <summary>operator +</summary>
    public static VoInt operator +(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value + y.m_value)));

    /// <summary>operator -</summary>
    public static VoInt operator -(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value - y.m_value)));

    /// <summary>operator *</summary>
    public static VoInt operator *(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value * y.m_value)));

    /// <summary>operator /</summary>
    public static VoInt operator /(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value / y.m_value)));

    /// <summary>operator %</summary>
    public static VoInt operator %(in VoInt x, in VoInt y)
        => IsUndefinedOrNull(x, y, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value % y.m_value)));


    //
    // ++, --, +, -, *, /, % operator    UGO.ValueArithmeticOperator
    //

    /// <summary>operator ++</summary>
    public static VoInt operator ++(in VoInt x)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value + 1)));

    /// <summary>operator --</summary>
    public static VoInt operator --(in VoInt x)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value - 1)));

    /// <summary>operator +</summary>
    public static VoInt operator +(in VoInt x, in int y)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value + y)));

    /// <summary>operator -</summary>
    public static VoInt operator -(in VoInt x, in int y)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value - y)));

    /// <summary>operator *</summary>
    public static VoInt operator *(in VoInt x, in int y)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value * y)));

    /// <summary>operator /</summary>
    public static VoInt operator /(in VoInt x, in int y)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value / y)));

    /// <summary>operator %</summary>
    public static VoInt operator %(in VoInt x, in int y)
        => IsUndefinedOrNull(x, out VoInt result)
            ? result
            : new VoInt(checked((int)(x.m_value % y)));


    //
    // TypeConverter
    //

    /// <summary>System.ComponentModel.TypeConverter</summary>
    private class VoIntTypeConverter : System.ComponentModel.TypeConverter
    {
        private static readonly Type WrapperType = typeof(VoInt);
        private static readonly Type ValueType = typeof(int);

        /// <summary>CanConvertFrom</summary>
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == WrapperType || sourceType == ValueType)
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>CanConvertTo</summary>
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == WrapperType || destinationType == ValueType)
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>ConvertFrom</summary>
        public override object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            if (value != null)
            {
                var t = value.GetType();
                if (t == typeof(VoInt))
                {
                    return (VoInt)value;
                }
                if (t == typeof(int))
                {
                    return new VoInt((int)value);
                }
            }
            else
            {
                return VoInt.NullValue;
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>ConvertTo</summary>
        public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is VoInt wrappedValue)
            {
                if (destinationType == WrapperType)
                {
                    return wrappedValue;
                }

                if (destinationType == ValueType)
                {
                    return wrappedValue.GetOrNull();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
