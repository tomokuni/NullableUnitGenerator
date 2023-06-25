using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace UnitGenerator.Tests;


[UnitOf(typeof(bool), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("boolean", example: "true")]
public readonly partial struct VoBool { }


[UnitOf(typeof(char), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoChar { }


[UnitOf(typeof(byte), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoByte { }


[UnitOf(typeof(sbyte), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoSbyte { }


[UnitOf(typeof(short), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoShort { }


[UnitOf(typeof(ushort), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUshort { }


[UnitOf(typeof(int), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoInt { }


[UnitOf(typeof(uint), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUint { }


[UnitOf(typeof(long), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoLong { }


[UnitOf(typeof(ulong), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("integer")]
public readonly partial struct VoUlong { }


[UnitOf(typeof(float), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoFloat { }


[UnitOf(typeof(double), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoDouble { }


[UnitOf(typeof(decimal), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("number")]
public readonly partial struct VoDecimal { }


[UnitOf(typeof(string), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoString { }


[UnitOf(typeof(Guid), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoGuid { }


[UnitOf(typeof(Ulid), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoUlid { }


[UnitOf(typeof(DateTime), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("datetime")]
public readonly partial struct VoDatetime { }


[UnitOf(typeof(DateOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("date")]
public readonly partial struct VoDateonly { }


[UnitOf(typeof(TimeOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("time")]
public readonly partial struct VoTimeonly { }


[UnitOf(typeof(TimeSpan), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("time")]
public readonly partial struct VoTimespan { }


[UnitOf(typeof(byte[]), UGO.GeneralOptions | UGO.JsonConverter | UGO.DapperTypeHandler)]
[UnitOfOas("string")]
public readonly partial struct VoByteArray { }  // base64 encoded characters
