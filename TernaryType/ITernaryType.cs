using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace Estable.TernaryType;


/// <summary>
/// ITernaryType 3状態を表現するインターフェイス ITernaryType (Undef, Null, Value)
/// </summary>
public interface ITernaryType : IEquatable<ITernaryType>
{
    //
    // get state property
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndef { get; }

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull { get; }

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue { get; }

    /// <summary>return value state.</summary>
    /// <returns><b>TernaryState.Undef</b><br/><b>TernaryState.Null</b><br/><b>TernaryState.Value</b></returns>
    public TernaryState State { get; }

    /// <summary>return value if HasValue is true; &lt;&lt;(null)&gt;&gt; if IsNull is true; &lt;&lt;(undef)&gt;&gt; if IsUndefined is true.</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>&lt;&lt;(null)&gt;&gt;</b> : if IsNull is true<br/><b>&lt;&lt;(undef)&gt;&gt;</b> : if IsUndefined is true</returns>
    public string ValueString { get; }


    //
    // ToString
    //

    /// <summary>Returns this instance of System.String; no actual conversion is performed.</summary>
    /// <returns>The current string.</returns>
    public virtual string ToString()
        => ValueString;


    //
    // Equals
    //

    /// <summary>Returns a value indicating whether this instance is same value to a specified value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public virtual bool Equals(ITernaryType? other)
        => other is not null &&
           ValueString == other.ValueString;

    /// <summary>Returns a value indicating whether this instance is same value to a specified value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public virtual bool Equals(object? obj)
        => Equals(obj as ITernaryType);

    ///// <summary>Returns a value indicating whether two instances are same value.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator ==(in ITernaryType x, in ITernaryType y)
    //    => x.Equals(y);

    ///// <summary>Returns a value indicates whether two instances are different values.</summary>
    ///// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    //public static bool operator !=(in ITernaryType x, in ITernaryType y)
    //    => !(x.Equals(y));

}
