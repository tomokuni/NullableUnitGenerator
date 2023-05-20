﻿#pragma warning disable CA1822	// メンバーを static に設定します

using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp.Builtins;

/// <param name="type">string, number, integer, boolean, ...</param>
/// <param name="format">type:number:(-, float, double), type:integer:(-, int32, int64)</param>
/// <param name="minimum">Parse to decimal</param>
/// <param name="exclusiveMinimum"></param>
/// <param name="maximum">Parse to decimal</param>
/// <param name="exclusiveMaximum"></param>
/// <param name="multipleOf">Parse to decimal</param>
/// <param name="minLength">Parse to int</param>
/// <param name="maxLength">Parse to int</param>
/// <param name="pattern"></param>
/// <param name="nullable"></param>
/// <param name="example"></param>
//public UnitOfOasAttribute(
//    string type,
//    string? format = null,
//    object? maximum = null,
//    bool exclusiveMinimum = false,
//    object? minimum = null,
//    bool exclusiveMaximum = false,
//    object? multipleOf = null,
//    object? maxLength = null,
//    object? minLength = null,
//    string? pattern = null,
//    object? example = null,
//    bool nullable = true)

[UnitOf(typeof(bool), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("boolean", example:"true")]
public readonly partial struct VoBool
{
    private partial void Validate() { }
}

[UnitOf(typeof(char), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "uint8")]
public readonly partial struct VoChar
{
    private partial void Validate() { }
}

[UnitOf(typeof(byte), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "int8")]
public readonly partial struct VoByte
{
    private partial void Validate() { }
}

[UnitOf(typeof(sbyte), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "uint8")]
public readonly partial struct VoSbyte
{
    private partial void Validate() { }
}

[UnitOf(typeof(short), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "int16")]
public readonly partial struct VoShort
{
    private partial void Validate() { }
}

[UnitOf(typeof(ushort), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "uint16")]
public readonly partial struct VoUshort
{
    private partial void Validate() { }
}

[UnitOf(typeof(int), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "int32")]
public readonly partial struct VoInt
{
    private partial void Validate() { }
}

[UnitOf(typeof(uint), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "uint32")]
public readonly partial struct VoUint
{
    private partial void Validate() { }
}

[UnitOf(typeof(long), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "int64")]
public readonly partial struct VoLong
{
    private partial void Validate() { }
}

[UnitOf(typeof(ulong), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer", format: "uint64")]
public readonly partial struct VoUlong
{
    private partial void Validate() { }
}

[UnitOf(typeof(float), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number", format: "float")]
public readonly partial struct VoFloat
{
    private partial void Validate() { }
}

[UnitOf(typeof(double), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number", format: "double")]
public readonly partial struct VoDouble
{
    private partial void Validate() { }
}

[UnitOf(typeof(decimal), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number", format: "decimal")]
public readonly partial struct VoDecimal
{
    private partial void Validate() { }
}

[UnitOf(typeof(string), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoString
{
    private partial void Validate() { }
}
