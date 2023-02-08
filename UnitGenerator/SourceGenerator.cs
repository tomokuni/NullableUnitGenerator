#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using NullableUnitGenerator.Template;
using NullableUnitGenerator.Helper;

namespace NullableUnitGenerator;


[Generator(LanguageNames.CSharp)]
public sealed class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(callback: GenerateInitialCode);

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "NullableUnitGenerator.UnitOfAttribute",    // 引っ掛ける属性のフルネーム
            static (node, token) => true,               // predicate, 属性で既に絞れてるので特別何かやりたいことがなければ基本true
            static (context, token) => context);        // GeneratorAttributeSyntaxContextにはNode, SemanticModel(Compilation), Symbolが入ってて便利
        context.RegisterSourceOutput(source, Emit);
    }

    void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        CancellationToken token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        // classで引っ掛けてるのでTypeSymbol/Syntaxとして使えるように。
        // SemaintiModelが欲しい場合は source.SemanticModel
        // Compilationが欲しい場合は source.SemanticModel.Compilation から
        var typeSymbol = (INamedTypeSymbol)source.TargetSymbol;
        var typeNode = (TypeDeclarationSyntax)source.TargetNode;
        var attrCtorArgs = source.Attributes.Single().ConstructorArguments;

        var ns = typeSymbol.ContainingNamespace;
        if (attrCtorArgs[0].Value is not ITypeSymbol type)
            throw new Exception("require UnitOf attribute parameter [Type]");
        var parsedOptions = Enum.ToObject(typeof(UnitGenerateOptions), (attrCtorArgs[1].Value ?? UnitGenerateOptions.None));

        var template = new CodeTemplate()
        {
            Name = typeSymbol.Name,
            Namespace = ns.IsGlobalNamespace ? null : ns.ToDisplayString(),
            Type = type.ToDisplayString(),
            IsValueType = type.IsValueType,
            Options = (UnitGenerateOptions)parsedOptions,
            ToStringFormat = attrCtorArgs[2].Value as string
        };
        var text = template.TransformText();

        token.ThrowIfCancellationRequested();

        string fullType = template.Namespace is null 
            ? $"{template.Name}" 
            : $"{template.Namespace}.{template.Name}";
        context.AddSource($"{fullType}.Generated.cs", text);

        token.ThrowIfCancellationRequested();
    }

    private void GenerateInitialCode(IncrementalGeneratorPostInitializationContext context)
    {
        CancellationToken token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        var template = new IOptionalTemplate();
        var text = template.TransformText();
        context.AddSource($"IOptional.Generated.cs", text);
        token.ThrowIfCancellationRequested();

        AddCsResource("UnitOfAttribute.cs");
        token.ThrowIfCancellationRequested();

        AddCsResource("UnitGenerateOptions.cs");
        token.ThrowIfCancellationRequested();

        AddCsResource("UnitOfOpenApiDataTypeAttribute.cs");
        token.ThrowIfCancellationRequested();

        void AddCsResource(string resourceName)
            => context.AddSource(hintName: resourceName, source: StringResourceRead(resourceName));
    }

    public static string StringResourceRead(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        //var type = typeof(SourceGenerator);
        //var assembly = type.Assembly;

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            return "";

        using var sr = new StreamReader(stream);
        return sr.ReadToEnd();
    }

}
