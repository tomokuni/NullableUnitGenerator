using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp.Others;


[UnitOf(typeof(Guid), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoGuid
{
    private partial void Validate() { }
}

[UnitOf(typeof(Ulid), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "uuid")]
public readonly partial struct VoUlid
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateTime), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "date-time")]
public readonly partial struct VoDatetime
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateOnly), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "date")]
public readonly partial struct VoDateonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeOnly), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "time")]
public readonly partial struct VoTimeonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeSpan), UGO.MaxExtentForDateTime | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "duration")]
public readonly partial struct VoTimespan
{
    private partial void Validate() { }
}

[UnitOf(typeof(byte[]), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
[UnitOfOas("string", format: "byte")]
public readonly partial struct VoByteArray  // base64 encoded characters
{
    private partial void Validate() { }
}
