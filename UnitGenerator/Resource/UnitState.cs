
using System;

namespace NullableUnitGenerator;


/// <summary>
/// 値の状態
/// </summary>
[Flags]
public enum UnitState
{
    /// <summary>Undefined State</summary>
    Undef = 0,

    /// <summary>Null State</summary>
    Null = 1,

    /// <summary>Value State</summary>
    Value = 3,

}

