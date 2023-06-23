#pragma warning disable CA1822	// メンバーを static に設定します

using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace ConsoleApp.Others;


[UnitOf(typeof(Guid), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoGuid { }


[UnitOf(typeof(Ulid), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoUlid { }


[UnitOf(typeof(DateTime), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("datetime")]
public readonly partial struct VoDatetime { }


[UnitOf(typeof(DateOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("date")]
public readonly partial struct VoDateonly { }


[UnitOf(typeof(TimeOnly), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("time")]
public readonly partial struct VoTimeonly { }


[UnitOf(typeof(TimeSpan), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("time")]
public readonly partial struct VoTimespan { }



[UnitOf(typeof(byte[]), UGO.GeneralOptions | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoByteArray { }  // base64 encoded characters
