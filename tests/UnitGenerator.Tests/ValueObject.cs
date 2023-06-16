#pragma warning disable CA1822	// メンバーを static に設定します

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MessagePack;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenOpts;

namespace UnitGenerator.Tests;


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


[UnitOf(typeof(bool), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("boolean", example: "true")]
public readonly partial struct VoBool { }


[UnitOf(typeof(char), UGO.MaxExtent | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "uint8")]
public readonly partial struct VoChar { }


[UnitOf(typeof(byte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "int8")]
public readonly partial struct VoByte { }


[UnitOf(typeof(sbyte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "uint8")]
public readonly partial struct VoSbyte { }


[UnitOf(typeof(short), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "int16")]
public readonly partial struct VoShort { }


[UnitOf(typeof(ushort), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "uint16")]
public readonly partial struct VoUshort { }


[UnitOf(typeof(int), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "int32")]
public readonly partial struct VoInt { }


[UnitOf(typeof(uint), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "uint32")]
public readonly partial struct VoUint { }


[UnitOf(typeof(long), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "int64")]
public readonly partial struct VoLong { }


[UnitOf(typeof(ulong), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer", format: "uint64")]
public readonly partial struct VoUlong { }


[UnitOf(typeof(float), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number", format: "float")]
public readonly partial struct VoFloat { }


[UnitOf(typeof(double), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number", format: "double")]
public readonly partial struct VoDouble { }


[UnitOf(typeof(decimal), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number", format: "decimal")]
public readonly partial struct VoDecimal { }


[UnitOf(typeof(string), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoString { }


[UnitOf(typeof(Guid), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoGuid { }


[UnitOf(typeof(Ulid), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoUlid { }


[UnitOf(typeof(DateTime), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "date-time")]
public readonly partial struct VoDatetime { }


[UnitOf(typeof(DateOnly), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "date")]
public readonly partial struct VoDateonly { }


[UnitOf(typeof(TimeOnly), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "time")]
public readonly partial struct VoTimeonly { }


[UnitOf(typeof(TimeSpan), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "duration")]
public readonly partial struct VoTimespan { }


[UnitOf(typeof(byte[]), UGO.MaxExtent | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string", format: "byte")]
public readonly partial struct VoByteArray { }  // base64 encoded characters
