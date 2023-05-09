using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypeInfo = Microsoft.CodeAnalysis.TypeInfo;

namespace NullableUnitGenerator;


internal static class NullableUnitGeneratorExtensions
{
    public static string GetTypeName(this ITypeSymbol typeSymbol)
    {
        // ITypeSymbol が配列の場合は配列内の型情報を対象に取得し、末尾に "[]" を付加する。
        if (typeSymbol is IArrayTypeSymbol { Rank: 1 } arrayTypeSymbol)
        {
            var elementType = arrayTypeSymbol.ElementType;
            var typeName = $"{elementType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}[]";
            return typeName;
        }
        // それ以外は ITypeSymbol から得る
        else
        {
            var typeName = $"{typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)}";
            return typeName;
        }
    }

    public static string GetTypeNameFull(this ITypeSymbol typeSymbol)
    {
        // ITypeSymbol が配列の場合は配列内の型情報を対象に取得し、末尾に "[]" を付加する。
        if (typeSymbol is IArrayTypeSymbol { Rank: 1 } arrayTypeSymbol)
        {
            var elementType = arrayTypeSymbol.ElementType;
            var typeNameFull = $"{elementType.ContainingNamespace}.{elementType.Name}[]";
            return typeNameFull;
        }
        // それ以外は ITypeSymbol から得る
        else
        {
            var typeNameFull = $"{typeSymbol.ContainingNamespace}.{typeSymbol.Name}";
            return typeNameFull;
        }
    }

}
