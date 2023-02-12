using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack.Resolvers;
using Newtonsoft.Json.Linq;
using TernaryUnitGenerator.Ternary;

namespace UnitGenerator.Tests.Ternary;


/// <summary>
/// TernaryStateStruct&lt;T&gt;
/// </summary>
public readonly record struct TernaryStateStruct<T> : IEquatable<TernaryStateStruct<T>>, ITernaryState where T : struct
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryStateStruct()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryStateStruct(T? value)
    {
        if (value is not null)
        {
            m_state = HAS_VALUE;
            m_value = (T)value;
        }
        else
        {
            m_state = NULL_VALUE;
            m_value = default(T);
        }
    }

    /// <summary>Complete Constructor</summary>
    public TernaryStateStruct(TernaryStateStruct<T> value)
    {
        m_state = HAS_VALUE;
        m_value = value.m_value;
    }


    //
    // static property
    //

    /// <summary>undefined value instance.</summary>
    public static TernaryStateStruct<T> UndefinedValue
        => new();

    /// <summary>null value instance.</summary>
    public static TernaryStateStruct<T> NullValue
        => new(null);

    /// <summary>default value instance.</summary>
    public static TernaryStateStruct<T> DefaultValue
        => new(default(T));

    /// <summary>default value instance.</summary>
    public static Type BaseType
        => typeof(T);


    //
    // static method
    //

    /// <summary>Determines if either value is undefined or null.</summary>
    /// <returns>
    /// <b>Return Value</b> – <b>out result Value</b> – Description<br/>
    /// <b>true</b> – <b>UndefinedValue</b> – if either value is undefined.<br/>
    /// <b>true</b> – <b>NullValue</b> – if either value is null.<br/>
    /// <b>false</b> – <b>DefaultValue</b> – if either value is set.
    /// </returns>
    public static byte CheckState(in TernaryStateStruct<T> x, in TernaryStateStruct<T> y)
        => (x, y) switch
        {
            { x.IsUndefined: true } => UNDEFINED_VALUE,
            { y.IsUndefined: true } => UNDEFINED_VALUE,
            { x.IsNull: true } => NULL_VALUE,
            { y.IsNull: true } => NULL_VALUE,
            _ => HAS_VALUE,
        };

    /// <summary>Determine if a value is undefined or null.</summary>
    /// <returns>
    /// <b>Return Value</b> – <b>out result Value</b> – Description<br/>
    /// <b>true</b> – <b>UndefinedValue</b> – if value is undefined.<br/>
    /// <b>true</b> – <b>NullValue</b> – if value is null.<br/>
    /// <b>false</b> – <b>DefaultValue</b> – if value is set.
    /// </returns>
    public static byte CheckState(in TernaryStateStruct<T> x)
        => x switch
        {
            { IsUndefined: true } => UNDEFINED_VALUE,
            { IsNull: true } => NULL_VALUE,
            _ => HAS_VALUE,
        };


    //
    // const
    //

    internal const byte UNDEFINED_VALUE = 0;
    internal const byte NULL_VALUE = 2;
    internal const byte HAS_VALUE = 3;
    public const string UNDEFINED_STRING = "<<(undefined)>>";
    public const string NULL_STRING = "<<(null)>>";


    //
    // backing field
    //

    internal readonly T m_value = default(T);
    internal readonly byte m_state = UNDEFINED_VALUE;


    //
    // get state property
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndefined 
        => m_state == UNDEFINED_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull 
        => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue
        => m_state == HAS_VALUE;

    /// <summary>return value state (UNDEFINED_VALUE or NULL_VALUE or HAS_VALUE).</summary>
    /// <returns><b>UNDEFINED_VALUE</b><br/><b>NULL_VALUE</b><br/><b>HAS_VALUE</b></returns>
    public byte State
        => m_state;

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value
        => HasValue ? m_value! : throw new InvalidOperationException("NoValue");

    /// <summary>return value if HasValue is true; &lt;null&gt; if IsNull is true; &lt;undefined&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;null&gt;</b> : if IsNull is true<br/><b>&lt;undefined&gt;</b> : if IsUndefined is true</returns>
    public string ValueString
        => HasValue ? m_value!.ToString()! : (IsNull ? NULL_STRING : UNDEFINED_STRING);

    //
    // GetHashCode
    //   is implemented on the record type
    //

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    //public override int GetHashCode()
    //    => new { m_state, m_value }.GetHashCode();

    //
    // ToString
    //

    /// <summary>Returns this instance of System.String; no actual conversion is performed.</summary>
    /// <returns>The current string.</returns>
    public override string ToString()
        => ValueString;


    //
    // IEquatable<TernaryStateStruct<T>>, Object.Equals
    //   is implemented on the record type
    //

    /// <summary>Returns a value indicating whether this instance is same value to a specified <#= Name #> value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public bool Equals(TernaryStateStruct<T> other)
    //    => m_value.Equals(other.m_value) &&
    //        IsNull.Equals(other.IsNull) &&
    //        IsUndefined.Equals(other.IsUndefined);

    /// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    /// <returns>true if obj is an instance of <#= Type #> or <#= Name #> and equals the value of this instance; otherwise, false.</returns>
    //public override bool Equals(object obj);
}
