using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
#pragma warning disable CS0108 // メンバーは継承されたメンバーを非表示にします。キーワード new がありません

namespace Estable.TernaryType;


/// <summary>
/// 3状態を表現するジェネリック版インターフェイス ITernaryStateValue&lt;T&gt; (Undef, Null, Value)
/// </summary>
public interface ITernaryType<T> where T : IEquatable<T>
{
    //
    // static property
    //

    /// <summary>default value instance.</summary>
    public static Type GetBaseType()
        => typeof(T);


    //
    // const
    //

    public const string DISPLAY_STRING_AS_UNDEF = "~undef~";
    public const string DISPLAY_STRING_AS_NULL = "~null~";


    //
    // get state
    //

    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndef { get; }

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull { get; }

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNullOrUndef { get; }

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsDefault { get; }

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue { get; }

    /// <summary>return value state.</summary>
    /// <returns><b>TernaryState.Undef</b><br/><b>TernaryState.Null</b><br/><b>TernaryState.Value</b></returns>
    public TernaryState State { get; }


    //
    // get value
    //

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value { get; }

    /// <inheritdoc cref="Value" />
    public T GetRawValue();

    //
    // Equals
    //

    public bool Equals(ITernaryType<T>? other);

    public bool Equals(ITernaryType<T>? x, ITernaryType<T>? y);

}
