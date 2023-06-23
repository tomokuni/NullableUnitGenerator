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


[UnitOf(typeof(bool), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("boolean", example: "true")]
public readonly partial struct VoBool { }


[UnitOf(typeof(char), UGO.MaxExtent | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoChar { }


[UnitOf(typeof(byte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoByte { }


[UnitOf(typeof(sbyte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoSbyte { }


[UnitOf(typeof(short), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoShort { }


[UnitOf(typeof(ushort), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUshort { }


[UnitOf(typeof(int), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoInt { }


[UnitOf(typeof(uint), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUint { }


[UnitOf(typeof(long), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoLong { }


[UnitOf(typeof(ulong), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUlong { }


[UnitOf(typeof(float), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoFloat { }


[UnitOf(typeof(double), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoDouble { }


[UnitOf(typeof(decimal), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoDecimal { }


[UnitOf(typeof(string), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoString { }


[UnitOf(typeof(Guid), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoGuid { }


[UnitOf(typeof(Ulid), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoUlid { }


[UnitOf(typeof(DateTime), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("datetime")]
public readonly partial struct VoDatetime { }


[UnitOf(typeof(DateOnly), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("date")]
public readonly partial struct VoDateonly { }


[UnitOf(typeof(TimeOnly), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("time")]
public readonly partial struct VoTimeonly { }


[UnitOf(typeof(TimeSpan), UGO.MaxExtentForDateTime | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.DapperTypeHandler)]
[UnitOfOas("time")]
public readonly partial struct VoTimespan { }


[UnitOf(typeof(byte[]), UGO.MaxExtent | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoByteArray { }  // base64 encoded characters
