
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        if (TypeSymbol is IArrayTypeSymbol { Rank: 1 } arrayTypeSymbol)
        {
            var elementType = arrayTypeSymbol.ElementType;
            TypeName = $"{elementType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}[]";
            TypeNameFull = $"{elementType.ContainingNamespace}.{elementType.Name}[]";
        }
        else
        {
            TypeName = $"{TypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}";
            TypeNameFull = $"{typeSymbol.ContainingNamespace}.{typeSymbol.Name}";
        }

        TypeMenberNames = TypeSymbol.GetMembers().Select(x => x.Name).Distinct().ToList();

        DicTypeName = new()
        {
            { 01, $"BaseType              : {TypeSymbol.BaseType}" },
            { 02, $"ContainingNamespace   : {TypeSymbol.ContainingNamespace}" },
            { 03, $"ContainingSymbol      : {TypeSymbol.ContainingSymbol}" },
            { 04, $"ContainingType        : {TypeSymbol.ContainingType}" },
            { 05, $"CanBeReferencedByName : {TypeSymbol.CanBeReferencedByName}" },
            { 06, $"Kind                  : {TypeSymbol.Kind}" },
            { 07, $"MetadataName          : {TypeSymbol.MetadataName}" },
            { 08, $"MetadataToken         : {TypeSymbol.MetadataToken}" },
            { 09, $"Name                  : {TypeSymbol.Name}" },
            { 10, $"NullableAnnotation    : {TypeSymbol.NullableAnnotation}" },
            { 11, $"OriginalDefinition    : {TypeSymbol.OriginalDefinition}" },
            { 12, $"SpecialType           : {TypeSymbol.SpecialType}" },
            { 13, $"TypeKind              : {TypeSymbol.TypeKind}" },
            { 14, $"ToDisplayString(CSharpErrorMessageFormat)      : {TypeSymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)}" },
            { 15, $"ToDisplayString(CSharpShortErrorMessageFormat) : {TypeSymbol.ToDisplayString(SymbolDisplayFormat.CSharpShortErrorMessageFormat)}" },
            { 16, $"ToDisplayString(FullyQualifiedFormat)          : {TypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}" },
            { 17 ,$"ToDisplayString(MinimallyQualifiedFormat)      : {TypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}" },
            { 18, $"IArrayTypeSymbol.ElementType : {(TypeSymbol as IArrayTypeSymbol)?.ElementType}" },
        };
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

    /// <summary>TypeMenbersNames list.</summary>
    private List<string> TypeMenberNames { get; }

    /// <summary>Type display string.</summary>
    internal string TypeName { get; }

    /// <summary>Type full name string.</summary>
    internal string TypeNameFull { get; }

    ///// <summary>type specified by the attribute.</summary>
    //internal Type Type { get; }


    /// <summary>Nullable type display string.</summary>
    internal string TypeNameNullable => $"{TypeName}{(IsValueType ? "?" : "")}";

    /// <summary>IsValueType</summary>
    internal bool IsValueType => TypeSymbol.IsValueType;

    /// <summary>IsArray</summary>
    internal bool IsArray => TypeSymbol.TypeKind == TypeKind.Array;


    /// <summary>ITypeSymbol value</summary>
    internal Dictionary<int, string> DicTypeName { get; }

    /// <summary>Operators string.</summary>
    internal string OperatorsString => string.Join(", ", TypeMenberNames);

    /// <summary>Specified operator is included or not.</summary>
    internal bool ContainsOperater(string operater) => TypeMenberNames.Contains(operater);

    /// <summary>Specified operator is included or not.</summary>
    internal bool ContainsOperater1(string operater)
    {
        if (!TypeMenberNames.Contains(operater))
            return false;

        var m = TypeSymbol.GetMembers().Where(x => x.Name == operater).ToList();
        foreach (var t in m.Select(x => x.GetType()))
        {
            var p = t.GetMethod(operater).GetParameters();
            if (p.Count() != 1)
                continue;
            if (p[0].ParameterType == t)
                return true;
        }
        return false;
    }

    /// <summary>Specified operator is included or not.</summary>
    internal bool ContainsOperater2(string operater)
    {
        if (!TypeMenberNames.Contains(operater))
            return false;

        var m = TypeSymbol.GetMembers().Where(x => x.Name == operater).ToList();
        foreach (var t in m.Select(x => x.GetType()))
        {
            var p = t.GetMethod(operater).GetParameters();
            if (p.Count() != 2)
                continue;
            if (p[0].ParameterType == t && p[1].ParameterType == t)
                return true;
        }
        return false;
    }


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
            "char" => true,
            "short" => true,
            "int" => true,
            "long" => true,
            "ushort" => true,
            "uint" => true,
            "ulong" => true,
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
            "DateTime" => true,
            "DateTimeOffset" => true,
            "TimeSpan" => true,
            "Guid" => true,
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

