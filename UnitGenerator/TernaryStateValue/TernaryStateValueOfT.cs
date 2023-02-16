using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
//#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
//#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace TernaryStateValue;


/// <summary>
/// 3状態を表現するジェネリック構造体 TernaryState&lt;T&gt;  (UNDEF, NULL, Value)
/// </summary>
public readonly record struct TernaryStateValue<T> : IEquatable<TernaryStateValue<T>>, ITernaryStateValue<T>, ITernaryStateValue
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryStateValue()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryStateValue(TernaryStateValue<T> value)
        => (m_state, m_value) = (value.m_state, value.m_value);

    /// <summary>Complete Constructor</summary>
    private TernaryStateValue((TernaryState state, T value) value)
        => (m_state, m_value) = value.state switch
        {
            TernaryState.Undef => (TernaryState.Undef, default(T)!),
            TernaryState.Null => (TernaryState.Null, default(T)!),
            _ => (TernaryState.Value, value.value)
        };

    /// <summary>Complete Constructor</summary>
    public TernaryStateValue(T? value)
        => (m_state, m_value) = value switch
        {
            not null => (TernaryState.Value, value),
            _ => (TernaryState.Null, default(T)!)
        };


    //
    // static property
    //
    
    /// <summary>undefined value instance.</summary>
    public static TernaryStateValue<T> GetUndefinedValue()
        => new((TernaryState.Undef, default(T)!));

    /// <summary>null value instance.</summary>
    public static TernaryStateValue<T> GetNullValue()
        => new((TernaryState.Null, default(T)!));

    /// <summary>default value instance.</summary>
    public static TernaryStateValue<T> GetDefaultValue()
        => new((TernaryState.Value, default(T)!));

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
    public static TernaryState CheckState(in TernaryStateValue<T> x, in TernaryStateValue<T> y)
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
    public static TernaryState CheckState(in TernaryStateValue<T> x)
        => x switch
        {
            { IsUndef: true } => TernaryState.Undef,
            { IsNull: true } => TernaryState.Null,
            _ => TernaryState.Value,
        };


    //
    // const
    //

    public const string UNDEF_STRING = "<<(undef)>>";
    public const string NULL_STRING = "<<(null)>>";


    //
    // backing field
    //

    internal readonly T m_value = default(T)!;
    internal readonly TernaryState m_state = TernaryState.Undef;


    //
    // get state property
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

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value
        => HasValue ? m_value! : throw new InvalidOperationException("NoValue");

    /// <summary>return value if HasValue is true; &lt;&lt;(null)&gt;&gt; if IsNull is true; &lt;&lt;(undef)&gt;&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;&lt;(null)&gt;&gt;</b> : if IsNull is true<br/><b>&lt;&lt;(undef)&gt;&gt;</b> : if IsUndefined is true</returns>
    public string ValueString
        => HasValue ? m_value!.ToString()! : (IsNull ? NULL_STRING : UNDEF_STRING);


    //
    // GetHashCode
    //   is implemented on the record type
    //

    ///// <summary>Returns the hash code for this instance.</summary>
    ///// <returns>A 32-bit signed integer hash code.</returns>
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
    // ITernaryState<T>
    //

    /// <summary>Returns a value indicating whether this instance is same value to a specified value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public bool Equals(ITernaryStateValue<T>? other)
        => other is not null &&
            IsUndef.Equals(other.IsUndef) &&
            IsNull.Equals(other.IsNull) &&
            Value!.Equals(other.Value);

    ///// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    ///// <returns>true if obj is an instance of <#= Type #> or <#= Name #> and equals the value of this instance; otherwise, false.</returns>
    //public override bool Equals(object obj);

}
