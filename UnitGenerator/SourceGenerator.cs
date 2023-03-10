#pragma warning disable CS8669  // Null 許容参照型の注釈は、'#nullable' 注釈のコンテキスト内のコードでのみ使用する必要があります。自動生成されたコードには、ソースに明示的な '#nullable' ディレクティブが必要です。
#pragma warning disable CS8632	// '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NullableUnitGenerator;


/// <summary>
/// ソースジェネレータ
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class SourceGenerator : IIncrementalGenerator
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
        var typeSymbol = (INamedTypeSymbol)source.TargetSymbol;
        var typeNode = (TypeDeclarationSyntax)source.TargetNode;
        var attrCtorArgs = source.Attributes.Single().ConstructorArguments;

        var ns = typeSymbol.ContainingNamespace;
        if (attrCtorArgs[0].Value is not ITypeSymbol type)
            throw new Exception("require UnitOf attribute parameter [Type]");
        var parsedOptions = Enum.ToObject(typeof(UnitGenerateOptions), (attrCtorArgs[1].Value ?? UnitGenerateOptions.None));

        var template = new Template.CodeTemplate()
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
        CancellationToken token = context.CancellationToken;
        token.ThrowIfCancellationRequested();

        //var template = new IOptionalTemplate();
        //var text = template.TransformText();
        //context.AddSource($"IOptional.Generated.cs", text);
        //token.ThrowIfCancellationRequested();

        //AddCsResource("UnitGenerateOptions.cs");
        //token.ThrowIfCancellationRequested();

        //AddCsResource("UnitOfAttribute.cs");
        //token.ThrowIfCancellationRequested();

        //AddCsResource("UnitOfHelper.cs");
        //token.ThrowIfCancellationRequested();

        //AddCsResource("UnitOfOpenApiDataTypeAttribute.cs");
        //token.ThrowIfCancellationRequested();

        ////
        //// ローカル関数
        ////
        //void AddCsResource(string resourceName)
        //    => context.AddSource(hintName: $"UnitOf.{resourceName}", source: StringResourceRead(resourceName));
    }

    /// <summary>
    /// 実行中のアセンブリに含まれるファイルを文字列として取得する
    /// </summary>
    /// <param name="resourceName"></param>
    /// <returns></returns>
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
