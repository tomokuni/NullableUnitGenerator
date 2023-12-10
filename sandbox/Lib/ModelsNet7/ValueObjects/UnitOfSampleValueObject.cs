using System;
using NullableUnitGenerator;

using UGO = NullableUnitGenerator.UnitGenerateOption;
using UST = NullableUnitGenerator.UnitSchemaType;

namespace NullableUnitGeneratorSample;


// Builtins

[UnitOf(typeof(bool), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Boolean, Example = true)]
public readonly partial struct VoBoolSample { }


[UnitOf(typeof(char), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.String)]
public readonly partial struct VoCharSample { }


[UnitOf(typeof(byte), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoByteSample { }


[UnitOf(typeof(sbyte), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoSbyteSample { }


[UnitOf(typeof(short), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoShortSample { }


[UnitOf(typeof(ushort), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoUshortSample { }


[UnitOf(typeof(int), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoIntSample { }


[UnitOf(typeof(uint), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoUintSample { }


[UnitOf(typeof(long), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoLongSample { }


[UnitOf(typeof(ulong), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int)]
public readonly partial struct VoUlongSample { }


[UnitOf(typeof(int), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int32)]
public readonly partial struct VoInt32Sample { }


[UnitOf(typeof(long), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Int64)]
public readonly partial struct VoInt64Sample { }


[UnitOf(typeof(float), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Number)]
public readonly partial struct VoFloatSample { }


[UnitOf(typeof(double), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Number)]
public readonly partial struct VoDoubleSample { }


[UnitOf(typeof(decimal), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Number)]
public readonly partial struct VoDecimalSample { }


[UnitOf(typeof(string), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.String)]
public readonly partial struct VoStringSample { }


// Others

[UnitOf(typeof(Guid), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.String)]
public readonly partial struct VoGuidSample { }


[UnitOf(typeof(Ulid), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.String)]
public readonly partial struct VoUlidSample { }


[UnitOf(typeof(DateTime), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Datetime)]
public readonly partial struct VoDatetimeSample { }


[UnitOf(typeof(DateOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Date)]
public readonly partial struct VoDateonlySample { }


[UnitOf(typeof(TimeOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Time)]
public readonly partial struct VoTimeonlySample { }


[UnitOf(typeof(TimeSpan), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Time)]
public readonly partial struct VoTimespanSample { }



[UnitOf(typeof(byte[]), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfSchema(UST.Byte)]
public readonly partial struct VoByteArraySample { }  // base64 encoded characters
