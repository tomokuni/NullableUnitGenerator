using System;
using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOptions;

namespace ConsoleApp
{
    [UnitOf(typeof(Guid), UGO.ParseMethod | UGO.Validate | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
    public readonly partial struct VoGuid
    {
        private partial void Validate() { }
    }

    [UnitOf(typeof(DateTime), UGO.ParseMethod | UGO.Validate | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
    public readonly partial struct VoDatetime
    {
        private partial void Validate() { }
    }
    [UnitOf(typeof(string), UGO.Validate | UGO.JsonConverter | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
    public readonly partial struct VoString
    {
        private partial void Validate() { }
    }
    [UnitOf(typeof(byte[]), UGO.Validate | UGO.DapperTypeHandler | UGO.EntityFrameworkValueConverter)]
    public readonly partial struct VoByteArray
    {
        private partial void Validate() { }
    }
}
