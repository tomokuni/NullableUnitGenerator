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
    //public static string GetFullName(this ITypeSymbol type)
    //{
    //    if (type.IsAnonymousType)
    //    {
    //        return type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).GetTypeName();
    //    }
    //    if (type is IArrayTypeSymbol)
    //    {
    //        var arrayType = (IArrayTypeSymbol)type;
    //        return arrayType.ElementType.GetFullName() + "[]";
    //    }

    //    var typeParameter = type as ITypeParameterSymbol;
    //    if (typeParameter != null)
    //    {
    //        return typeParameter.Name;
    //    }
    //    else
    //    {
    //        string result = type.MetadataName;
    //        if (type.ContainingType != null)
    //            result = type.ContainingType.GetFullName() + "." + result;
    //        else if (!type.ContainingNamespace.IsGlobalNamespace)
    //            result = type.ContainingNamespace.GetFullName() + "." + result;
    //        return result;
    //    }
    //}
}
