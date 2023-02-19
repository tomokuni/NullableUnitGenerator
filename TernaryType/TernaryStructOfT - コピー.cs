using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
//#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
//#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace Estable.TernaryType;


/// <summary>
/// 3状態を表現するジェネリック構造体 TernaryState&lt;T&gt;  (Undef, Null, Value)
/// </summary>
public readonly struct TernaryStruct<T> : IEquatable<TernaryStruct<T>>, IEqualityComparer<TernaryStruct<T>> where T : struct, IEquatable<T>
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryStruct()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryStruct(TernaryStruct<T> value)
        => (m_state, m_value) = (value.m_state, value.m_value);

    /// <summary>Complete Constructor</summary>
    public TernaryStruct(TernaryState state, T value = default)
        => (m_state, m_value) = state switch
        {
            TernaryState.Undef => (TernaryState.Undef, default),
            TernaryState.Null => (TernaryState.Null, default),
            _ => (TernaryState.Value, value)
        };

    /// <summary>Complete Constructor</summary>
    public TernaryStruct(T value)
        => (m_state, m_value) = (TernaryState.Value, value);

    /// <summary>Complete Constructor</summary>
    public TernaryStruct(T? value)
        => (m_state, m_value) = value switch
        {
            not null => (TernaryState.Value, (T)value),
            _ => (TernaryState.Null, default)
        };


    //
    // static property
    //
    
    /// <summary>undefined value instance.</summary>
    public static TernaryStruct<T> UndefValue
        => new(TernaryState.Undef, default);

    /// <summary>null value instance.</summary>
    public static TernaryStruct<T> NullValue
        => new(TernaryState.Null, default);

    /// <summary>default value instance.</summary>
    public static TernaryStruct<T> DefaultValue
        => new(TernaryState.Value, default);

    /// <summary>default value instance.</summary>
    public static Type GetBaseType()
        => typeof(T);


    //
    // static method
    //

    /// <summary>Determines if either value is undefined or null.</summary>
    /// <returns>
    /// Return Value – Description<br/>
    /// <b>TernaryState.Undef</b> – if either value is undefined.<br/>
    /// <b>TernaryState.Null</b> – if either value is null.<br/>
    /// <b>TernaryState.Value</b> – if either value is set.
    /// </returns>
    public static TernaryState CheckState(in TernaryStruct<T> x, in TernaryStruct<T> y)
        => (x, y) switch
        {
            { x.IsUndef: true } => TernaryState.Undef,
            { y.IsUndef: true } => TernaryState.Undef,
            { x.IsNull: true } => TernaryState.Null,
            { y.IsNull: true } => TernaryState.Null,
            _ => TernaryState.Value,
        };

    /// <summary>Determine if a value is undefined or null.</summary>
    /// <returns>
    /// Return Value – Description<br/>
    /// <b>TernaryState.Undef</b> – if value is undefined.<br/>
    /// <b>TernaryState.Null</b> – if value is null.<br/>
    /// <b>TernaryState.Value</b> – if value is set.
    /// </returns>
    public static TernaryState CheckState(in TernaryStruct<T> x)
        => x switch
        {
            { IsUndef: true } => TernaryState.Undef,
            { IsNull: true } => TernaryState.Null,
            _ => TernaryState.Value,
        };


    //
    // const
    //

    public const string DISPLAY_STRING_AS_UNDEF = "~undef~";
    public const string DISPLAY_STRING_AS_NULL = "~null~";


    //
    // backing field
    //

    internal readonly T m_value = default;
    internal readonly TernaryState m_state = TernaryState.Undef;


    //
    // get state
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndef 
        => m_state == TernaryState.Undef;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull 
        => m_state == TernaryState.Null;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue
        => m_state == TernaryState.Value;

    /// <summary>return value state.</summary>
    /// <returns><b>TernaryState.Undef</b><br/><b>TernaryState.Null</b><br/><b>TernaryState.Value</b></returns>
    public TernaryState State
        => m_state;


    //
    // get value
    //

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value
        => GetOrThrow();

    /// <summary>return value if HasValue is true; &lt;&lt;(null)&gt;&gt; if IsNull is true; &lt;&lt;(undef)&gt;&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;&lt;(null)&gt;&gt;</b> : if IsNull is true<br/><b>&lt;&lt;(undef)&gt;&gt;</b> : if IsUndefined is true</returns>
    public string ValueString
        => HasValue ? m_value.ToString()! : (IsNull ? DISPLAY_STRING_AS_NULL : DISPLAY_STRING_AS_UNDEF);

    /// <inheritdoc cref="Value" />
    public T GetOrThrow()
        => HasValue ? m_value : throw new InvalidOperationException(IsNull ? DISPLAY_STRING_AS_NULL : DISPLAY_STRING_AS_UNDEF);

    /// <summary>return value if HasValue is true; otherwise, <see langword="default(T)"/></summary>
    /// <returns><b>value</b> : if assigned and not null<br/><b><see langword="default(T)"/></b> : otherwise</returns>
    public T GetOrDefault(bool treatUndefAsNull = false)
        => HasValue ? m_value : ((IsNull || treatUndefAsNull) ? default : throw new InvalidOperationException(DISPLAY_STRING_AS_UNDEF));

    /// <summary>return value if HasValue is true; otherwise, defaultValue</summary>
    /// <returns><b>value</b> : if assigned and not null<br/><b>defaultValue</b> : otherwise</returns>
    public T? GetOrDefault(T? defaultValue, bool treatUndefAsNull = false)
        => HasValue ? m_value : ((IsNull || treatUndefAsNull) ? defaultValue : throw new InvalidOperationException(DISPLAY_STRING_AS_UNDEF));

    public T GetOrDefault(T defaultValue, bool treatUndefAsNull = false)
        => HasValue ? m_value : ((IsNull || treatUndefAsNull) ? defaultValue : throw new InvalidOperationException(DISPLAY_STRING_AS_UNDEF));

    /// <summary>return value if HasValue is true; otherwise, null</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b><see langword="null"/></b> : otherwise</returns>
    public T? GetOrNull(bool treatUndefAsNull = false)
        => HasValue? m_value : ((IsNull || treatUndefAsNull) ? null : throw new InvalidOperationException(DISPLAY_STRING_AS_UNDEF));

    /// <summary>
    /// return true and out parameter value if HasValue is true; otherwise, false.
    /// </summary>
    /// <param name="value">value</param>
    /// <returns><b><see langword="true"/> and out parameter value</b> : if HasValue is true,</returns>
    public bool TryGet(out T value)
    {
        value = m_value;
        return HasValue;
    }


    //
    // GetHashCode
    //   is implemented on the record type
    //

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => (m_state, m_value).GetHashCode();

    public int GetHashCode([DisallowNull] TernaryStruct<T> obj)
        => (obj.m_state, obj.m_value).GetHashCode();


    //
    // ToString
    //

    /// <summary>Returns this instance of System.String; no actual conversion is performed.</summary>
    /// <returns>The current string.</returns>
    public override string ToString()
        => ValueString;


    //
    // implicit, explicit operator
    //

    ///// <summary>explicit operator</summary>
    ///// <returns>T? value.</returns>
    //public static explicit operator T?(TernaryStruct<T> value)
    //    => !value.IsUndef ? value.GetOrNull() : throw new InvalidOperationException(DISPLAY_STRING_AS_UNDEF);

    /// <summary>implicit operator</summary>
    /// <returns>TernaryStruct&lt;T&gt; value.</returns>
    public static implicit operator TernaryStruct<T>(T? value)
        => new(value);

    ///// <summary>implicit operator</summary>
    ///// <returns>TernaryStruct&lt;T&gt; value.</returns>
    //public static implicit operator TernaryStruct<T>(T value)
    //    => new(value);


    //
    // Equals
    //

    public bool Equals(TernaryStruct<T> other)
        => m_state.Equals(other.m_state) && m_value.Equals(other.m_value);

    /// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    /// <returns>true if obj is an instance of <#= Type #> or <#= Name #> and equals the value of this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => obj is TernaryStruct<T> ts ? Equals(ts) : base.Equals(obj);

    public bool Equals(TernaryStruct<T> x, TernaryStruct<T> y)
        => x.Equals(y.m_state) && x.Equals(y.m_value);


    //
    // ==, != operator
    //

    /// <summary>Returns a value indicating whether two instances are same value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator ==(in TernaryStruct<T> x, in TernaryStruct<T> y)
        => x.Equals(y);

    /// <summary>Returns a value indicates whether two instances are different values.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator !=(in TernaryStruct<T> x, in TernaryStruct<T> y)
        => !(x.Equals(y));

    ///// <summary>Returns a value indicating whether two instances are same value.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator ==(in TernaryStruct<T> x, in object? y)
    //    => x.Equals(y);

    ///// <summary>Returns a value indicates whether two instances are different values.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator !=(in TernaryStruct<T> x, in object? y)
    //    => !(x.Equals(y));

    ///// <summary>Returns a value indicating whether two instances are same value.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator ==(in object? x, in TernaryStruct<T> y)
    //    => y.Equals(x);

    ///// <summary>Returns a value indicates whether two instances are different values.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator !=(in object? x, in TernaryStruct<T> y)
    //    => !(y.Equals(x));

}
