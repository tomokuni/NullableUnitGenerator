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
public interface ITernaryType<T>
{
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
    /// <returns>
    /// <b>Undef</b><br/>
    /// <b>Null</b><br/>
    /// <b>Value</b>
    /// </returns>
    public TernaryState State { get; }


    //
    // get value
    //

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException()</summary>
    /// <returns>
    /// <b>value</b> : if HasValue is true<br/>
    /// <b>throw InvalidOperationException("IsNull")</b> : if IsNull is true<br/>
    /// <b>throw InvalidOperationException("IsUndef")</b> : if IsUndef is true
    /// </returns>
    public T Value { get; }

    /// <summary>return raw value</summary>
    /// <returns>raw value</returns>
    [return: MaybeNull] 
    public T GetRawValue();

    /// <summary>return value if HasValue is true; otherwise, defaultValue</summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b>defaultValue</b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOr([AllowNull] T? defaultValue = default);

    /// <summary>return value if HasValue is true; otherwise, <see langword="default(T)"/></summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b><see langword="default(T)"/></b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOrDefault();

    /// <summary>return value if HasValue is true; otherwise, <see langword="null"/></summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b><see langword="null"/></b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOrNull();

    /// <inheritdoc cref="Value" />
    [return: NotNull]
    public T GetOrThrow();

    /// <summary>
    /// return true and out parameter value if HasValue is true; otherwise, false.
    /// </summary>
    /// <param name="value">value</param>
    /// <param name="defaultValue">defaultValue</param>
    /// <returns><b><see langword="true"/> and out parameter value</b> : if HasValue is true,</returns>
    public bool TryGet(out T value, [AllowNull] T defaultValue = default);

}
