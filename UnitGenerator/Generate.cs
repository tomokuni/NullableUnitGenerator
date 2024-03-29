﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

using Microsoft.CodeAnalysis;

namespace NullableUnitGenerator;


/// <summary>
/// ソースジェネレータ
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class Generate : IIncrementalGenerator
{
    /// <summary>
    /// ソース生成の開始ポイント
    /// </summary>
    /// <param name="context"></param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 特に処理せずにソース生成するものは先に実施しておく
        context.RegisterPostInitializationOutput(callback: GenerateInitialCode);

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "NullableUnitGenerator.UnitOfAttribute",    // 引っ掛ける属性のフルネーム
            static (node, token) => true,               // predicate, 属性で既に絞れてるので特別何かやりたいことがなければ基本true
            static (context, token) => context);        // GeneratorAttributeSyntaxContextにはNode, SemanticModel(Compilation), Symbolが入ってて便利
        context.RegisterSourceOutput(source, Emit);     // 複雑になるので、メソッドを分ける
    }

    /// <summary>
    /// 分割したソース生成の実行部分
    /// </summary>
    /// <param name="context"></param>
    /// <param name="source"></param>
    /// <exception cref="Exception"></exception>
    void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        CancellationToken token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        // classで引っ掛けてるのでTypeSymbol/Syntaxとして使えるように。
        // SemaintiModelが欲しい場合は source.SemanticModel
        // Compilationが欲しい場合は source.SemanticModel.Compilation から
        var targetSymbol = (INamedTypeSymbol)source.TargetSymbol;
        //var typeNode = (TypeDeclarationSyntax)source.TargetNode;
        var attrCtorArgs = source.Attributes.Single().ConstructorArguments;

        var ns = targetSymbol.ContainingNamespace;
        if (attrCtorArgs[0].Value is not ITypeSymbol typeSymbol)
            throw new Exception("require UnitOf attribute parameter [Type]");
        var parsedOptions = Enum.ToObject(typeof(UnitGenerateOption), (attrCtorArgs[1].Value ?? UnitGenerateOption.None));

        var template = new CodeTemplate(
            ns: ns.IsGlobalNamespace ? null : ns.ToDisplayString(), 
            name: targetSymbol.Name,
            typeSymbol: typeSymbol,
            options: (UnitGenerateOption)parsedOptions,
            toStringFormat: attrCtorArgs[2].Value as string);
        var text = template.TransformText().Replace("\r\n", "\n");

        token.ThrowIfCancellationRequested();

        string fullType = template.Namespace is null 
            ? $"NoNamespace.{template.Name}" 
            : $"{template.Namespace}.{template.Name}";
        context.AddSource($"{fullType}.g.cs", text);

        token.ThrowIfCancellationRequested();
    }

    /// <summary>
    /// 特に処理せずにソース生成するものは先に実施しておく
    /// </summary>
    /// <param name="context"></param>
    private void GenerateInitialCode(IncrementalGeneratorPostInitializationContext context)
    {
        AddCsResource("UnitOf.IUnit.cs");
        AddCsResource("UnitOf.UnitAttribute.cs");
        AddCsResource("UnitOf.UnitSchemaAttribute.cs");
        AddCsResource("UnitOf.UnitSchemaType.cs");
        AddCsResource("UnitOf.UnitGenerateOption.cs");
        AddCsResource("UnitOf.UnitExtension.cs");
        AddCsResource("UnitOf.UnitExtension.OpenApi.cs");
        AddCsResource("UnitOf.UnitHelper.cs");
        AddCsResource("UnitOf.UnitState.cs");
        AddCsResource("UnitOf.UnitValidate.cs");
        
        //
        // ローカル関数
        //
        void AddCsResource(string resourceName)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            context.AddSource(hintName: $"{resourceName}", source: StringResourceRead(resourceName));
        }
    }

    /// <summary>
    /// 実行中のアセンブリに含まれるファイルを文字列として取得する
    /// </summary>
    /// <param name="resourceName"></param>
    /// <returns></returns>
    public static string StringResourceRead(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
            using var sr = new StreamReader(stream);
            return sr.ReadToEnd();
        }

        throw new ApplicationException($"The specified resource [{resourceName}] is missing.");
    }

}
