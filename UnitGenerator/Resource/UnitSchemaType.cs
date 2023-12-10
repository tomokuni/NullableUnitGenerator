using System;
using System.Collections.Generic;
using System.Text;

namespace NullableUnitGenerator;


/// <summary>
/// UnitSchemaType
/// </summary>
public enum UnitSchemaType
{
    /// <summary>string.</summary>
    String,

    /// <summary>integer.</summary>
    Int,

    /// <summary>signed 32 bits integer.</summary>
    Int32,

    /// <summary>signed 64 bits integer.</summary>
    Int64,

    /// <summary>number.</summary>
    Number,

    /// <summary>float number.</summary>
    Float,

    /// <summary>double number.</summary>
    Double,

    /// <summary>true or false.</summary>
    Boolean,

    /// <summary>date string.</summary><example>9999-19-39</example>
    Date,

    /// <summary>time string.</summary><example>T29:59:59Z</example>
    Time,

    /// <summary>datetime string.</summary><example>9999-19-39T29:59:59Z</example>
    Datetime,

    /// <summary>phone number string.</summary>
    Phone,

    /// <summary>email address string.</summary>
    Email,

    /// <summary>url string.</summary>
    Url,

    /// <summary>base64 encoded characters.</summary>
    Byte,

}
