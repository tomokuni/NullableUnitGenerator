#if !UGO_OPENAPI_DISABLE
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NullableUnitGenerator;


/// <summary>
/// NullableUnitGeneratorExtensions for OpenApiSpecification
/// </summary>
public static class NullableUnitGeneratorExtensions
{
    // Set OpenApiSchema to SwaggerGenOptions based on the UnitOfOpenApiDataType attribute.
    // UnitOfOpenApiDataType属性を元に、SwaggerGenOptions に OpenApiSchema を設定する。

    /// <summary>
    /// Search for UnitOfOas attribute and register it as Schema information in SwaggerGenOptions.<br/>
    /// UnitOfOas 属性を探索し、SwaggerGenOptions に Schema 情報として登録する。<br/>
    /// </summary>
    /// <param name="options">SwaggerGenOptions</param>
    /// <returns>SwaggerGenOptions</returns>
    public static SwaggerGenOptions MapTypeUnitOfOas(this SwaggerGenOptions options)
    {
        // UnitOfOas 属性が付与されたクラスと属性を取得
        var ta = UnitHelper.GetTypeAndAttributes<UnitOfOasAttribute>();
        foreach (var (type, attr) in ta)
        {
            options.MapType(type, attr.ToOpenApiSchema);
        }
        return options;
    }


    /// <summary>
    /// Convert to an OpenApiSchema object based on the UnitOfOas attribute.
    /// UnitOfOas 属性を元に、OpenApiSchema オブジェクトに変換する。<br/>
    /// </summary>
    /// <param name="attr">UnitOfOasAttribute</param>
    /// <returns>OpenApiSchema</returns>
    public static OpenApiSchema ToOpenApiSchema(this UnitOfOasAttribute attr)
    {
        IOpenApiAny exampleAny = attr.Example switch
        {
            null => new OpenApiNull(),
            int integer => new OpenApiInteger(integer),
            double floating => new OpenApiDouble(floating),
            var e => new OpenApiString(e.ToString()),
        };

        var schema = new OpenApiSchema
        {
            Type = attr.Type,
            Format = attr.Format,
            Minimum = attr.Minimum is null ? null : decimal.Parse(attr.Minimum),
            Maximum = attr.Maximum is null ? null : decimal.Parse(attr.Maximum),
            MinLength = attr.MinLength,
            MaxLength = attr.MaxLength,
            Pattern = attr.Pattern,
            Example = exampleAny,
            Nullable = attr.Nullable,
        };

        return schema;
    }

}
#endif

