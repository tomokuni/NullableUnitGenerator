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
/// TernaryStateClass&lt;T&gt;
/// </summary>
public readonly record struct TernaryStateClass<T> : ITernaryState<T>, ITernaryState where T : class
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryStateClass()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryStateClass(TernaryStateClass<T> value)
        => (m_state, m_value) = (HAS_VALUE, value.m_value);

    /// <summary>Complete Constructor</summary>
    public TernaryStateClass(T? value)
        => (m_state, m_value) = value switch
        {
            not null => (HAS_VALUE, (T)value),
            _ => (NULL_VALUE, default(T)),
        };


    //
    // static property
    //

    /// <summary>undefined value instance.</summary>
    public static ITernaryState<T> UndefinedValue
        => new TernaryStateClass<T>();

    /// <summary>null value instance.</summary>
    public static ITernaryState<T> NullValue
        => new TernaryStateClass<T>((T?)null);

    /// <summary>default value instance.</summary>
    public static ITernaryState<T> DefaultValue
        => new TernaryStateClass<T>(default(T));

    /// <summary>default value instance.</summary>
    public static Type BaseType
        => typeof(int);


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
    public static bool IsUndefinedOrNull(in TernaryStateClass<T> x, in TernaryStateClass<T> y, out ITernaryState<T> result)
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

    /// <summary>Determine if a value is undefined or null.</summary>
    /// <returns>
    /// <b>Return Value</b> – <b>out result Value</b> – Description<br/>
    /// <b>true</b> – <b>UndefinedValue</b> – if value is undefined.<br/>
    /// <b>true</b> – <b>NullValue</b> – if value is null.<br/>
    /// <b>false</b> – <b>DefaultValue</b> – if value is set.
    /// </returns>
    public static bool IsUndefinedOrNull(in TernaryStateClass<T> x, out ITernaryState<T> result)
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

    ///// <summary>Create object from ValueString.</summary>
    ///// <param name="value">ValueString</param>
    ///// <returns>TernaryStateValue object</returns>
    //public static TernaryStateValue Create(string value)
    //{
    //    return value switch
    //    {
    //        UNDEFINED_STRING => UndefinedValue,
    //        NULL_STRING => NullValue,
    //        _ => new(int.Parse(value))
    //    };
    //}


    //
    // const
    //

    internal const byte UNDEFINED_VALUE = 0;
    internal const byte NULL_VALUE = 1;
    internal const byte HAS_VALUE = 3;
    public const string UNDEFINED_STRING = "<undefined>";
    public const string NULL_STRING = "<null>";


    //
    // backing field
    //

    internal readonly T? m_value = default(T);
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

}
