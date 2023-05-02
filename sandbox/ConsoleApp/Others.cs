using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp.Others;


[UnitOf(typeof(Guid), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoGuid
{
    private partial void Validate() { }
}

[UnitOf(typeof(Ulid), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoUlid
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateTime), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDatetime
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateOnly), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDateonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeOnly), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoTimeonly
{
    private partial void Validate() { }
}

[UnitOf(typeof(TimeSpan), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoTimespan
{
    private partial void Validate() { }
}

[UnitOf(typeof(byte[]), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
public readonly partial struct VoByteArray
{
    private partial void Validate() { }
}
