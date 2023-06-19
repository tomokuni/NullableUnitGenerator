using System;

namespace NullableUnitGenerator;


/// <summary>
/// Value Status
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

