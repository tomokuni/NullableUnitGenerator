﻿#pragma warning disable CA1822	// メンバーを static に設定します

using System.Runtime.CompilerServices;

using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenOpts;

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

[UnitOf(typeof(bool), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("boolean", example:"true")]
public readonly partial struct VoBool { }


[UnitOf(typeof(char), UGO.MaxExtent | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoChar { }


[UnitOf(typeof(byte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoByte { }



[UnitOf(typeof(sbyte), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoSbyte { }


[UnitOf(typeof(short), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoShort { }


[UnitOf(typeof(ushort), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoUshort { }


[UnitOf(typeof(int), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoInt { }


[UnitOf(typeof(uint), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoUint { }


[UnitOf(typeof(long), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoLong { }


[UnitOf(typeof(ulong), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("integer")]
public readonly partial struct VoUlong { }


[UnitOf(typeof(float), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number")]
public readonly partial struct VoFloat { }


[UnitOf(typeof(double), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number")]
public readonly partial struct VoDouble { }


[UnitOf(typeof(decimal), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("number")]
public readonly partial struct VoDecimal { }


[UnitOf(typeof(string), UGO.MaxExtent | UGO.JsonConverter | UGO.JsonConverterDictionaryKey | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
[UnitOfOas("string")]
public readonly partial struct VoString { }

