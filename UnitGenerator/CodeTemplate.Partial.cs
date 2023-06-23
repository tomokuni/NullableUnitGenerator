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
    /// <param name="options">UnitGenOpts specified by the attribute</param>
    /// <param name="toStringFormat">ToStringFormat value specified by the attribute</param>
    public CodeTemplate(string? ns, string name, ITypeSymbol typeSymbol, UnitGenerateOption options, string? toStringFormat)
    {
        Namespace = ns;
        Name = name;
        TypeSymbol = typeSymbol;
        Options = options;
        ToStringFormat = toStringFormat;

        TypeName = typeSymbol.GetTypeName();
        TypeNameFull = typeSymbol.GetTypeNameFull();

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

    /// <summary>UnitGenOpts specified by the attribute.</summary>
    internal UnitGenerateOption Options { get; }

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
    internal string TypeNameNullable
        => $"{TypeName}{(IsValueType ? "?" : "")}";

    /// <summary>IsValueType</summary>
    internal bool IsValueType
        => TypeSymbol.IsValueType;

    /// <summary>IsArray</summary>
    internal bool IsArray
        => TypeSymbol.TypeKind == TypeKind.Array;

    /// <summary>Is the type name DateOnly.</summary>
    internal bool IsDateOnly
        => TypeName == "DateOnly";

    /// <summary>Is the type name TimeOnly.</summary>
    internal bool IsTimeOnly
        => TypeName == "TimeOnly";

    /// <summary>Is the type name Ulid.</summary>
    internal bool IsUlid
        => TypeName == "Ulid" || TypeName == "System.Ulid";


    /// <summary>ITypeSymbol value</summary>
    internal Dictionary<int, string> DicTypeName { get; }

    /// <summary>Specified operator is included or not.</summary>
    internal bool ContainsOperater(string operater)
        => TypeMenberNames.Contains(operater);

    /// <summary>Operators string</summary>
    internal string OperatorsString
        => string.Join(", ", TypeMenberNames);


    /// <summary>HasArithmeticIncDecOperator</summary>
    internal bool HasArithmeticIncDecOperator
        => IsBuiltinNumericType 
           || (ContainsOperater("op_Increment") && ContainsOperater("op_Decrement")) ;

    /// <summary>HasArithmeticAddSubOperator</summary>
    internal bool HasArithmeticAddSubOperator
        => IsBuiltinNumericType
           || (ContainsOperater("op_Addition") && ContainsOperater("op_Subtraction") && TypeName != "DateTime");

    /// <summary>HasArithmeticMulDevModOperator</summary>
    internal bool HasArithmeticMulDevModOperator
        => IsBuiltinNumericType
           || (ContainsOperater("op_Multiply") && ContainsOperater("op_Division") && ContainsOperater("op_Modulus"));

    /// <summary>HasComparisonOperator</summary>
    internal bool HasComparisonOperator
        => IsBuiltinNumericType
           || (ContainsOperater("op_GreaterThan") && ContainsOperater("op_LessThan") && ContainsOperater("op_GreaterThanOrEqual") && ContainsOperater("op_LessThanOrEqual"));

    /// <summary>HasParseMethod</summary>
    internal bool HasParseMethod
        => IsBuiltinNumericType
           || (ContainsOperater("Parse") && ContainsOperater("TryParse"));

    /// <summary>HasCompareToMethod</summary>
    internal bool HasCompareToMethod
        => IsBuiltinNumericType
           || (ContainsOperater("CompareTo"));

    /// <summary>HasMinMaxMethod</summary>
    internal bool HasMinMaxMethod
        => IsBuiltinNumericType
           || (ContainsOperater("Min") && ContainsOperater("Max"));


    /// <summary>Specified UnitGenOpts value is included or not.</summary>
    internal bool HasFlag(UnitGenerateOption options)
        => Options.HasFlag(options);


    /// <summary>Integer type or not.</summary>
    internal bool IsBuiltinIntegralType
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
    internal bool IsBuiltinFloatingType
        => TypeName switch
        {
            "float" => true,
            "double" => true,
            "decimal" => true,
            _ => false
        };

    /// <summary>Numeric type or not.</summary>
    internal bool IsBuiltinNumericType
        => IsBuiltinIntegralType || IsBuiltinFloatingType;

    /// <summary>Is Utf8 formatter support or not.</summary>
    internal bool IsSupportUtf8Formatter()
        => TypeName switch
        {
            "char" => false,
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
            "bool" => true,
            "float" => true,
            "double" => true,
            "decimal" => true,
            "DateTime" => true,
            "DateTimeOffset" => true,
            "DateOnly" => false,
            "TimeOnly" => false,
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
            "DateTime" => DbType.DateTime,
            "DateTimeOffset" => DbType.DateTimeOffset,
            "DateOnly" => DbType.Date,
            "TimeOnly" => DbType.Time,
            "TimeSpan" => DbType.Time,
            "Guid" => DbType.Guid,
            _ => DbType.Object
        };

}

