using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
//#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
//#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace NullableUnitGenerator.TernaryState;


/// <summary>
/// 3状態を表現するジェネリック構造体 TernaryState&lt;T&gt;  (UNDEF, NULL, Value)
/// </summary>
public readonly record struct TernaryState<T> : IEquatable<TernaryState<T>>, ITernaryState<T>, ITernaryState
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryState()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryState(TernaryState<T> value)
        => (m_state, m_value) = (value.m_state, value.m_value);

    /// <summary>Complete Constructor</summary>
    private TernaryState((byte state, T value) value)
        => (m_state, m_value) = value.state switch
        {
            UNDEF_VALUE => (UNDEF_VALUE, default(T)!),
            NULL_VALUE => (NULL_VALUE, default(T)!),
            _ => (HAS_VALUE, value.value)
        };

    /// <summary>Complete Constructor</summary>
    public TernaryState(T? value)
        => (m_state, m_value) = value switch
        {
            not null => (HAS_VALUE, value),
            _ => (NULL_VALUE, default(T)!)
        };


    //
    // static property
    //
    
    /// <summary>undefined value instance.</summary>
    public static TernaryState<T> UndefinedValue
        => new((UNDEF_VALUE, default(T)!));

    /// <summary>null value instance.</summary>
    public static TernaryState<T> NullValue
        => new((NULL_VALUE, default(T)!));

    /// <summary>default value instance.</summary>
    public static TernaryState<T> DefaultValue
        => new((HAS_VALUE, default(T)!));

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
    public static byte CheckState(in TernaryState<T> x, in TernaryState<T> y)
        => (x, y) switch
        {
            { x.IsUndef: true } => UNDEF_VALUE,
            { y.IsUndef: true } => UNDEF_VALUE,
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
    public static byte CheckState(in TernaryState<T> x)
        => x switch
        {
            { IsUndef: true } => UNDEF_VALUE,
            { IsNull: true } => NULL_VALUE,
            _ => HAS_VALUE,
        };


    //
    // const
    //

    internal const byte UNDEF_VALUE = 0;
    internal const byte NULL_VALUE = 1;
    internal const byte HAS_VALUE = 3;
    public const string UNDEF_STRING = "<<(undef)>>";
    public const string NULL_STRING = "<<(null)>>";


    //
    // backing field
    //

    internal readonly T m_value = default(T)!;
    internal readonly byte m_state = UNDEF_VALUE;


    //
    // get state property
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndef 
        => m_state == UNDEF_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull 
        => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue
        => m_state == HAS_VALUE;

    /// <summary>return value state (UNDEF_VALUE or NULL_VALUE or HAS_VALUE).</summary>
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
        => HasValue ? m_value!.ToString()! : (IsNull ? NULL_STRING : UNDEF_STRING);

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
    // ITernaryState<T>
    //

    /// <summary>Returns a value indicating whether this instance is same value to a specified <#= Name #> value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public bool Equals(ITernaryState<T>? other)
        => other is not null &&
            IsUndef.Equals(other.IsUndef) &&
            IsNull.Equals(other.IsNull) &&
            Value!.Equals(other.Value);

    /// <summary>Returns a value indicating whether this instance is same value to a specified object.</summary>
    /// <returns>true if obj is an instance of <#= Type #> or <#= Name #> and equals the value of this instance; otherwise, false.</returns>
    //public override bool Equals(object obj);

}
