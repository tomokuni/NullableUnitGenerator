using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MessagePack;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp.Builtins;


[UnitOf(typeof(bool), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoBool
{
    private partial void Validate() {
        Console.WriteLine();
    }
}

[UnitOf(typeof(byte), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoByte
{
    private partial void Validate() { }
}

[UnitOf(typeof(sbyte), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoSbyte
{
    private partial void Validate() { }
}

[UnitOf(typeof(char), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoChar
{
    private partial void Validate() { }
}

[UnitOf(typeof(decimal), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoDecimal
{
    private partial void Validate() { }
}

[UnitOf(typeof(double), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoDouble
{
    private partial void Validate() { }
}

[UnitOf(typeof(float), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoFloat
{
    private partial void Validate() { }
}

[UnitOf(typeof(int), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoInt
{
    private partial void Validate() { }
}

[UnitOf(typeof(uint), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoUint
{
    private partial void Validate() { }
}

[UnitOf(typeof(nint), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoNint
{
    private partial void Validate() { }
}

[UnitOf(typeof(nuint), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoNuint
{
    private partial void Validate() { }
}

[UnitOf(typeof(long), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoLong
{
    private partial void Validate() { }
}

[UnitOf(typeof(ulong), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoUlong
{
    private partial void Validate() { }
}

[UnitOf(typeof(short), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoShort
{
    private partial void Validate() { }
}

[UnitOf(typeof(ushort), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoUshort
{
    private partial void Validate() { }
}

[UnitOf(typeof(string), UGO.MaxExtent | UGO.Validate | UGO.JsonConverterSupport | UGO.JsonConverterDictionaryKeySupport | UGO.MessagePackFormatterSupport | UGO.DapperTypeHandlerSupport | UGO.EntityFrameworkValueConverterSupport)]
public readonly partial struct VoString
{
    private partial void Validate() { }
}
