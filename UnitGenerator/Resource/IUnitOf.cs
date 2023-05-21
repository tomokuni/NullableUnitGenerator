﻿#pragma warning disable CS8601  // Null 参照代入の可能性があります。

using System;

namespace NullableUnitGenerator;


/// <summary>
/// UnitOf属性を付けたクラスに付与されるインターフェイス
/// </summary>
public interface IUnitOf
{
    /// <summary><see langword="true"/> if undefined; otherwise.</summary>
    public bool IsUndef { get; }

    /// <summary><see langword="true"/> if null; otherwise.</summary>
    public bool IsNull { get; }

    /// <summary><see langword="true"/> if undefined or null; otherwise.</summary>
    public bool IsUndefOrNull { get; }

    /// <summary><see langword="true"/> if null or hasValue; otherwise.</summary>
    public bool IsNullOrHasValue { get; }

    /// <summary><see langword="true"/> if not undefined and not null; otherwise.</summary>
    public bool HasValue { get; }

    /// <summary>return value state.</summary>
    public UnitState State { get; }


#if NET7_0_OR_GREATER
    /// <summary>base type.</summary>
    abstract public static Type BaseType { get; }

    /// <summary>Is base type nullable?</summary>
    abstract public static bool IsNullable { get; }
#endif

}


/// <summary>
/// UnitOf&lt;T>属性を付けたクラスに付与されるインターフェイス
/// </summary>
public interface IUnitOf<T, U> : IUnitOf
{
    /// <summary>return value if HasValue is true; otherwise, throw InvalidOperationException()</summary>
    public U Value { get; }


#if NET7_0_OR_GREATER
    //
    // static property
    //

    /// <summary>Undefined value instance.</summary>
    abstract public static T UndefValue { get; }

    /// <summary>Null value instance.</summary>
    abstract public static T NullValue { get; }

    /// <summary>Value state default value instance.</summary>
    abstract public static T DefaultValueOfValueState { get; }

    //
    // ==, != operator
    //

    ///// <summary>Returns a value indicating whether two instances are same value.</summary>
    //abstract public static bool operator ==(in T x, in T y);

    ///// <summary>Returns a value indicates whether two instances are different values.</summary>
    //abstract public static bool operator !=(in T x, in T y);
#endif

}
