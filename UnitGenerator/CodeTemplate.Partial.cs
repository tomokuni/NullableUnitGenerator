
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NullableUnitGenerator;


public partial class CodeTemplate
{
    /// <summary>
    /// Complete Constructor
    /// </summary>
    /// <param name="ns">Namespace</param>
    /// <param name="name">Value-object name</param>
    /// <param name="typeSymbol">type specified by the attribute</param>
    /// <param name="options">UnitGenerateOptions specified by the attribute</param>
    /// <param name="toStringFormat">ToStringFormat value specified by the attribute</param>
    public CodeTemplate(string? ns, string name, ITypeSymbol typeSymbol, UnitGenerateOptions options, string? toStringFormat)
    {
        Namespace = ns;
        Name = name;
        TypeSymbol = typeSymbol;
        Options = options;
        ToStringFormat = toStringFormat;
    }

    /// <summary>Namespace</summary>
    internal string? Namespace { get; }

    /// <summary>Value-object name</summary>
    internal string Name { get; }

    /// <summary>type specified by the attribute.</summary>
    internal ITypeSymbol TypeSymbol { get; }

    /// <summary>UnitGenerateOptions specified by the attribute.</summary>
    internal UnitGenerateOptions Options { get; }

    /// <summary>ToStringFormat value specified by the attribute.</summary>
    internal string? ToStringFormat { get; }


    /// <summary>Type display string.</summary>
    internal string TypeName => TypeSymbol.ToDisplayString();

    /// <summary>Nullable type display string.</summary>
    internal string TypeNameNullable => $"{TypeName}{(IsValueType ? "?" : "")}";

    /// <summary>IsValueType</summary>
    internal bool IsValueType => TypeSymbol.IsValueType;

    /// <summary>IsArray</summary>
    internal bool IsArray => TypeName.EndsWith("[]");


    /// <summary>Operators list.</summary>
    private List<string> Operators = new();

    internal List<string> GetOperators()
    {
        if (!Operators.Any())
        {
            Operators = TypeSymbol
                .GetMembers()
                .Select(x => x.Name)
                .Distinct().ToList();
        }
        return Operators;
    }

    /// <summary>Operators string.</summary>
    internal string OperatorsString => string.Join(", ", GetOperators());

    /// <summary>Specified operator is included or not.</summary>
    internal bool ContainsOperater(string operater) => GetOperators().Contains(operater);


    /// <summary>Specified UnitGenerateOptions value is included or not.</summary>
    internal bool HasFlag(UnitGenerateOptions options)
        => Options.HasFlag(options);


    /// <summary>Is the type name Ulid.</summary>
    internal bool IsUlid
        => TypeName == "Ulid" || TypeName == "System.Ulid";


    /// <summary>Integer type or not.</summary>
    internal bool IsIntegralType
        => TypeName switch
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
            "nint" => true,
            "nuint" => true,
            _ => false
        };

    /// <summary>Floating point type or not.</summary>
    internal bool IsFloatingType
        => TypeName switch
        {
            "float" => true,
            "double" => true,
            "decimal" => true,
            _ => false
        };

    /// <summary>Numeric type or not.</summary>
    internal bool IsNumericType
        => IsIntegralType || IsFloatingType;

    /// <summary>Is Utf8 formatter support or not.</summary>
    internal bool IsSupportUtf8Formatter()
        => TypeName switch
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


    /// <summary>Get DBType.</summary>
    internal DbType GetDbType()
        => TypeName switch
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
            "decimal" => DbType.Currency,
            "System.DateTime" => DbType.DateTime,
            "System.DateTimeOffset" => DbType.DateTimeOffset,
            "System.TimeSpan" => DbType.Time,
            "System.Guid" => DbType.Guid,
            _ => DbType.Object
        };

}

