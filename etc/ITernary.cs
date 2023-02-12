using System;

namespace NullableUnitGenerator.Disable;


public interface ITernary<T>
{
    /// <summary>undefined value instance.</summary>
    static object? UndefinedValue { get; }
    //public static Option UndefinedValue
    //    => new();

    /// <summary>null value instance.</summary>
    static object? NullValue { get; }
    //public static Option NullValue
    //    => new((T?)null);

    /// <summary>default value instance.</summary>
    static object? DefaultValue { get; }
    //public static Option DefaultValue
    //    => new(default(T));

    /// <summary>default value instance.</summary>
    static Type? PrimitiveType { get; }
    //public static Type PrimitiveType
    //    => typeof(T);


    //internal const byte UNDEFINED_VALUE = 0;
    //internal const byte NULL_VALUE = 1;
    //internal const byte HAS_VALUE = 3;

    //internal readonly T m_value = default(T);
    //internal readonly byte m_state = UNDEFINED_VALUE;


    /// <summary><see langword="true"/> if undefined; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if undefined</returns>
    public bool IsUndefined { get; } // => m_state == UNDEFINED_VALUE;

    /// <summary><see langword="true"/> if null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if null</returns>
    public bool IsNull { get; } // => m_state == NULL_VALUE;

    /// <summary><see langword="true"/> if not undefined and not null; otherwise, <see langword="false"/>.</summary>
    /// <returns><b><see langword="true"/></b> : if not undefined and not null</returns>
    public bool HasValue { get; }
    //    => m_state == HAS_VALUE;


    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException("NoValue")</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b>throw InvalidOperationException("NoValue")</b> : otherwise</returns>
    public T Value { get; }
    //    => HasValue ? m_value : throw new InvalidOperationException("NoValue");

    /// <inheritdoc cref="Value" />
    public T AsPrimitive();
    //    => Value;

    /// <summary>return value if HasValue is true; otherwise, <see langword="default(T)"/></summary>
    /// <returns><b>value</b> : if assigned and not null<br/><b><see langword="default(T)"/></b> : otherwise</returns>
    public T GetOrDefault();
    //    => HasValue ? m_value : default(T);

    /// <summary>return value if HasValue is true; otherwise, defaultValue</summary>
    /// <returns><b>value</b> : if assigned and not true<br/><b>defaultValue</b> : otherwise</returns>
    /// <!-- T? 型にしたいけど、継承できないので object? で代用する -->
    public object? GetOrDefault(object? defaultValue);
    //    => HasValue ? m_value : defaultValue;
    ///// <inheritdoc cref="OrDefault" />
    //public int? OrDefault(int? defaultValue)
    //    => HasValue ? m_value : defaultValue;

    /// <summary>return value if HasValue is true; otherwise, null</summary>
    /// <returns><b>value</b> : if HasValue is true<br/><b><see langword="null"/></b> : otherwise</returns>
    /// <!-- T? 型にしたいけど、継承できないので object? で代用する -->
    public object? GetOrNull();
    //    => (T?)(HasValue ? m_value : null);

    /// <inheritdoc cref="Value" />
    public T GetOrThrow();
    //    => Value;

    /// <summary>
    /// return true and out parameter value if HasValue is true; otherwise, false.
    /// </summary>
    /// <param name="value">value</param>
    /// <returns><b><see langword="true"/> and out parameter value</b> : if HasValue is true,</returns>
    public bool TryGet(out T value);
    //{
    //    value = m_value;
    //    return HasValue;
    //}

}
