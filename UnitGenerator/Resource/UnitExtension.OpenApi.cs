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
    /// Search for UnitOfSchema attribute and register it as Schema information in SwaggerGenOptions.<br/>
    /// UnitOfSchema 属性を探索し、SwaggerGenOptions に Schema 情報として登録する。<br/>
    /// </summary>
    /// <param name="options">SwaggerGenOptions</param>
    /// <returns>SwaggerGenOptions</returns>
    public static SwaggerGenOptions MapTypeUnitOfSchema(this SwaggerGenOptions options)
    {
        // UnitOfSchema 属性が付与されたクラスと属性を取得
        var ta = UnitHelper.GetTypeAndAttributes<UnitOfSchemaAttribute>();
        foreach (var (type, attr) in ta)
        {
            options.MapType(type, attr.ToOpenApiSchema);
        }
        return options;
    }


    /// <summary>
    /// Convert to an OpenApiSchema object based on the UnitOfSchema attribute.
    /// UnitOfSchema 属性を元に、OpenApiSchema オブジェクトに変換する。<br/>
    /// </summary>
    /// <param name="attr">UnitOfSchemaAttribute</param>
    /// <returns>OpenApiSchema</returns>
    public static OpenApiSchema ToOpenApiSchema(this UnitOfSchemaAttribute attr)
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
            Title = attr.Title,
            Description = attr.Description,
            Example = exampleAny,
            Maximum = attr.Maximum,
            ExclusiveMaximum = attr.ExclusiveMaximum,
            Minimum = attr.Minimum,
            ExclusiveMinimum = attr.ExclusiveMinimum,
            MaxLength = attr.MaxLength,
            MinLength = attr.MinLength,
            Pattern = attr.Pattern,
            Nullable = attr.Nullable,
        };

        return schema;
    }

}
#endif

