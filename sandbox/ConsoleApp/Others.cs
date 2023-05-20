#pragma warning disable CA1822	// メンバーを static に設定します

using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp.Others;


[UnitOf(typeof(Guid), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoGuid
{
    private partial void Validate() { }
}

[UnitOf(typeof(Ulid), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoUlid
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateTime), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "date-time")]
public readonly partial struct VoDatetime
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateOnly), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "date")]
public readonly partial struct VoDateonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeOnly), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "time")]
public readonly partial struct VoTimeonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeSpan), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "duration")]
public readonly partial struct VoTimespan
{
    private partial void Validate() { }
}

[UnitOf(typeof(byte[]), UGO.MaxExtent | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string", format: "byte")]
public readonly partial struct VoByteArray  // base64 encoded characters
{
    private partial void Validate() { }
}
