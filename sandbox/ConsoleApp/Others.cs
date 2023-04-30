using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp;


[UnitOf(typeof(Guid), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoGuid
{
    private partial void Validate() { }
}

[UnitOf(typeof(DateTime), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoDatetime
{
    private partial void Validate() { }
}
[UnitOf(typeof(string), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoString
{
    private partial void Validate() { }
}
[UnitOf(typeof(byte[]), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
public readonly partial struct VoByteArray
{
    private partial void Validate() { }
}
