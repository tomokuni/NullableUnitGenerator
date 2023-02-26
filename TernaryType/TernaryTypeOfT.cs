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
public readonly struct TernaryTypeS<T> : IEquatable<TernaryTypeS<T>>, IEqualityComparer<TernaryTypeS<T>>  where T : struct, IEquatable<T>
{
    //
    // Constructor
    //

    /// <summary>Complete Constructor</summary>
    public TernaryTypeS()
    {
    }

    /// <summary>Complete Constructor</summary>
    public TernaryTypeS(in TernaryTypeS<T> value)
        => (m_state, m_value) = (value.m_state, value.m_value);

    /// <summary>Complete Constructor</summary>
    public TernaryTypeS(in TernaryState state, [AllowNull] in T value = default)
        => (m_state, m_value) = (state, value) switch
        {
            (TernaryState.Undef, _) => (state, default),
            (TernaryState.Null, _) => (state, default),
            //(_, null) => (state, default),
            _ => (state, value)
        };

    /// <summary>Complete Constructor</summary>
    public TernaryTypeS([DisallowNull] T value)
        => (m_state, m_value) = (TernaryState.Value, value);

    /// <summary>Complete Constructor</summary>
    public TernaryTypeS([AllowNull] T? value)
        => (m_state, m_value) = value switch
        {
            null => (TernaryState.Null, default),
            _ => (TernaryState.Value, value.Value),
        };


    //
    // static property
    //

    /// <summary>String representing Undef.</summary>
    public static readonly string sUndef = $"~{TernaryState.Undef}~";

    /// <summary>String representing Null.</summary>
    public static readonly string sNull = $"~{TernaryState.Null}~";

    //
    // static property
    //

    /// <summary>Undefined value instance.</summary>
    public static TernaryTypeS<T> UndefValue { get; } = new(TernaryState.Undef, default);

    /// <summary>Null value instance.</summary>
    public static TernaryTypeS<T> NullValue { get; } = new(TernaryState.Null, default);

    /// <summary>Value state default value instance.</summary>
    public static TernaryTypeS<T> ValueStateDefaultValue { get; } = new(TernaryState.Value, default);

    static readonly Type BaseType = typeof(T);
    /// <summary>Get base type.</summary>
    public static Type GetBaseType() => BaseType;

    /// <summary>Is base type nullable?</summary>
    public static bool IsNullable { get; } 
        = !BaseType.IsValueType  // 値型でない -> Nullable
        || Nullable.GetUnderlyingType(BaseType) != null;  // 非Null許容型が取得できる -> Null許容演算子が指定されている -> Nullable


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
    public static TernaryState CheckState(in TernaryTypeS<T> x, in TernaryTypeS<T> y)
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
    public static TernaryState CheckState(in TernaryTypeS<T> x)
        => x switch
        {
            { IsUndef: true } => TernaryState.Undef,
            { IsNull: true } => TernaryState.Null,
            _ => TernaryState.Value,
        };


    //
    // backing field
    //

    [AllowNull] internal readonly T m_value = default;
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

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNullOrUndef
        => m_state != TernaryState.Value;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue
        => m_state == TernaryState.Value;

    /// <summary>return value state.</summary>
    /// <returns>
    /// <b>Undef</b><br/>
    /// <b>Null</b><br/>
    /// <b>Value</b>
    /// </returns>
    public TernaryState State
        => m_state;


    //
    // get value
    //

    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException()</summary>
    /// <returns>
    /// <b>value</b> : if HasValue is true<br/>
    /// <b>throw InvalidOperationException("Value is Null.")</b> : if IsNull is true<br/>
    /// <b>throw InvalidOperationException("Value is Undef.")</b> : if IsUndef is true
    /// </returns>
    public T Value
        => GetOrThrow();

    /// <summary>return raw value</summary>
    /// <returns>raw value</returns>
    [return: MaybeNull]
    public T GetRawValue()
        => m_value;

    /// <summary>return value if HasValue is true; otherwise, defaultValue</summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b>defaultValue</b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOr([AllowNull] T? defaultValue = default)
        => HasValue ? m_value : defaultValue;

    /// <inheritdoc/>
    [return: MaybeNull]
    public T GetOr([AllowNull] T defaultValue = default)
        => HasValue ? m_value : defaultValue;

    /// <summary>return value if HasValue is true; otherwise, <see langword="default(T)"/></summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b><see langword="default(T)"/></b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOrDefault()
        => GetOr(default);

    /// <summary>return value if HasValue is true; otherwise, <see langword="null"/></summary>
    /// <returns>
    /// <b>value</b> : if assigned and not null<br/>
    /// <b><see langword="null"/></b> : otherwise
    /// </returns>
    [return: MaybeNull]
    public T? GetOrNull()
        => GetOr(null);

    /// <inheritdoc cref="Value" />
    [return: NotNull]
    public T GetOrThrow()
        => (m_state, m_value) switch
        {
            (TernaryState.Value, _) => m_value,
            (TernaryState.Null, _) => throw new InvalidOperationException($"Value is {TernaryState.Null}."),
            _ => throw new InvalidOperationException($"Value is {TernaryState.Undef}."),
        };

    /// <summary>
    /// return true and out parameter value if HasValue is true; otherwise, false.
    /// </summary>
    /// <param name="value">value</param>
    /// <param name="defaultValue">defaultValue</param>
    /// <returns><b><see langword="true"/> and out parameter value</b> : if HasValue is true,</returns>
    public bool TryGet(out T value, T defaultValue = default)
    {
        value = HasValue ? m_value : defaultValue;
        return HasValue;
    }


    //
    // GetHashCode
    //

    /// <inheritdoc/>
    public override int GetHashCode()
        => (m_state, m_value).GetHashCode();

    /// <inheritdoc/>
    public int GetHashCode(TernaryTypeS<T> obj)
        => (obj.m_state, obj.m_value).GetHashCode();


    //
    // ToString
    //

    /// <summary>Returns this instance of System.String; no actual conversion is performed.</summary>
    /// <returns>The current string.</returns>
    public override string ToString()
        => ValueString;

    /// <summary>return value string.</summary>
    /// <returns>
    /// <b>ValueString</b> : if HasValue is true<br/>
    /// <b>"~Null~"</b> : if IsNull is true<br/>
    /// <b>"~Undef~"</b> : if IsUndefined is true
    /// </returns>
    string ValueString
        => HasValue ? $"{m_value}" : (IsNull ? sNull : sUndef);


    //
    // implicit, explicit operator
    //

    /// <summary>explicit(明示的) operator</summary>
    /// <returns>T? value.</returns>
    [return: MaybeNull]
    public static explicit operator T(TernaryTypeS<T> value)
        => value.GetOrThrow();

    /// <summary>explicit(明示的) operator</summary>
    /// <returns>T? value.</returns>
    [return: MaybeNull]
    public static explicit operator T?(TernaryTypeS<T> value)
        => value.GetOrNull();

    /// <summary>implicit(暗黙的) operator</summary>
    /// <returns>TernaryType&lt;T&gt; value.</returns>
    public static implicit operator TernaryTypeS<T>([AllowNull] T value)
        => new(value);

    /// <summary>implicit(暗黙的) operator</summary>
    /// <returns>TernaryType&lt;T&gt; value.</returns>
    public static implicit operator TernaryTypeS<T>([AllowNull] T? value)
        => new(value);



    //
    // Equals
    //

    /// <inheritdoc/>
    public bool Equals(TernaryTypeS<T> other)
        => m_state == other.m_state && m_value.Equals(other.m_value);

    /// <inheritdoc/>
    public bool Equals(TernaryTypeS<T> x, TernaryTypeS<T> y)
        => x.Equals(y);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is TernaryTypeS<T> ts && Equals(ts);


    //
    // ==, != operator
    //

    /// <summary>Returns a value indicating whether two instances are same value.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator ==(in TernaryTypeS<T> x, in TernaryTypeS<T> y)
        => x.Equals(y);

    /// <summary>Returns a value indicates whether two instances are different values.</summary>
    /// <returns>true if other has the same value as this instance; otherwise, false.</returns>
    public static bool operator !=(in TernaryTypeS<T> x, in TernaryTypeS<T> y)
        => !(x == y);

}
