using System;
using System.Data;

namespace NullableUnitGenerator
{
    public partial class CodeTemplate
    {
        internal string? Namespace { get; set; }
        internal string? Type { get; set; }
        internal string? Name { get; set; }
        internal Type? SystemType { get => GetSystemType(); }
        internal string? TypeNullable { get => $"{Type}{(IsValueType() ? "?" : "")}"; }
        internal UnitGenerateOptions Options { get; set; }
        public string? ToStringFormat { get; set; }

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

        internal Type? GetSystemType()
            => Type switch
            {
                "short" => typeof(short),
                "int" => typeof(int),
                "long" => typeof(long),
                "ushort" => typeof(ushort),
                "uint" => typeof(uint),
                "ulong" => typeof(ulong),
                "bool" => typeof(bool),
                "byte" => typeof(byte),
                "sbyte" => typeof(sbyte),
                "float" => typeof(float),
                "double" => typeof(double),
                "decimal" => typeof(decimal),
                "System.DateTime" => typeof(DateTime),
                "System.DateTimeOffset" => typeof(DateTimeOffset),
                "System.TimeSpan" => typeof(TimeSpan),
                "System.Guid" => typeof(Guid),
                "string" => typeof(string),
                "byte[]" => typeof(byte[]),
                _ => null
            };

        internal bool IsUlid()
            => Type == "Ulid" || Type == "System.Ulid";

        internal bool IsValueType()
            => (SystemType != null && SystemType.IsValueType) || IsUlid();

        internal bool IsNullable()
            => SystemType == null ||
               !SystemType.IsValueType ||
               Nullable.GetUnderlyingType(SystemType) != null;
    }
}

