using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace TernaryStateValue;


/// <summary>
/// 3状態を表現するジェネリック版インターフェイス ITernaryStateValue&lt;T&gt; (Undef, Null, Value)
/// </summary>
public interface ITernaryStateValue<T> : IEquatable<ITernaryStateValue<T>>, ITernaryStateValue
{
    //
    // Constructor
    //


    //
    // static property
    //


    //
    // static method
    //


    //
    // const
    //


    //
    // backing field
    //


    //
    // get state property
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndef { get; }
    //    => m_state == UNDEF_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull { get; }
    //    => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue { get; }
    //    => m_state == HAS_VALUE;

    /// <summary>return value state.</summary>
    /// <returns><b>TernaryState.Undef</b><br/><b>TernaryState.Null</b><br/><b>TernaryState.Value</b></returns>
    public TernaryState State { get; }
    //    => m_state;

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value { get; }
    //    => HasValue ? m_value! : throw new InvalidOperationException("NoValue");

    /// <summary>return value if HasValue is true; &lt;&lt;(null)&gt;&gt; if IsNull is true; &lt;&lt;(undef)&gt;&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;&lt;(null)&gt;&gt;</b> : if IsNull is true<br/><b>&lt;&lt;(undef)&gt;&gt;</b> : if IsUndefined is true</returns>
    public string ValueString { get; }
    //    => HasValue ? m_value!.ToString()! : (IsNull ? NULL_STRING : UNDEF_STRING);


    //
    // GetHashCode
    //   is implemented on the record type
    //


    //
    // ToString
    //


    //
    // IEquatable<TernaryStateStruct<T>>, Object.Equals
    //   is implemented on the record type
    //

}
