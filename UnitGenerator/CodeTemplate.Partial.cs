using System;
using System.Data;

namespace NullableUnitGenerator;


public partial class CodeTemplate
{
    internal string? Namespace { get; set; }
    internal string? Name { get; set; }
    internal string? Type { get; set; }
    internal bool IsValueType { get; set; }
    internal string? TypeNullable { get => $"{Type}{(IsValueType ? "?" : "")}"; }
    internal UnitGenerateOptions Options { get; set; }
    internal string? ToStringFormat { get; set; }

    internal bool HasFlag(UnitGenerateOptions options)
        => Options.HasFlag(options);

    internal DbType GetDbType()
        => Type switch
        {
            "short" => DbType.Int16,
            "int" => DbType.Int32,
            "long" => DbType.Int64,
            "ushort" => DbType.UInt16,
            "uint" => DbType.UInt32,
            "ulong" => DbType.UInt64,
            "string" => DbType.AnsiString,
            "byte[]" => DbType.Binary,
            "bool" => DbType.Boolean,
            "byte" => DbType.Byte,
            "sbyte" => DbType.SByte,
            "float" => DbType.Single,
            "double" => DbType.Double,
            "System.DateTime" => DbType.DateTime,
            "System.DateTimeOffset" => DbType.DateTimeOffset,
            "System.TimeSpan" => DbType.Time,
            "System.Guid" => DbType.Guid,
            "decimal" => DbType.Currency,
            _ => DbType.Object
        };

    internal bool IsSupportUtf8Formatter()
        => Type switch
        {
            "short" => true,
            "int" => true,
            "long" => true,
            "ushort" => true,
            "uint" => true,
            "ulong" => true,
            "bool" => true,
            "byte" => true,
            "sbyte" => true,
            "float" => true,
            "double" => true,
            "System.DateTime" => true,
            "System.DateTimeOffset" => true,
            "System.TimeSpan" => true,
            "System.Guid" => true,
            _ => false
        };

    internal bool IsIntegralNumericType()
        => Type switch
        {
            "short" => true,
            "int" => true,
            "long" => true,
            "ushort" => true,
            "uint" => true,
            "ulong" => true,
            "bool" => true,
            "byte" => true,
            "sbyte" => true,
            _ => false
        };

    internal bool IsUlid()
        => Type == "Ulid" || Type == "System.Ulid";
}

