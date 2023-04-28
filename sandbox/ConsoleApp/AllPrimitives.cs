using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp
{
    [UnitOf(typeof(int), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoInt
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(uint), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoUint
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(short), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoShort
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(ushort), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoUshort
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(byte), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoByte
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(sbyte), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoSbyte
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(float), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoFloat
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(double), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoDouble
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(decimal), UGO.PrivitiveFull | UGO.Validate | UGO.JsonConverter | UGO.MessagePackFormatter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter | UGO.JsonConverterDictionaryKeySupport)]
    public readonly partial struct VoDecimal
    {
        private partial void Validate() { }
    }
}
