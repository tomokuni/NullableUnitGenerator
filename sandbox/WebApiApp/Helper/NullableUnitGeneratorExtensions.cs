using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace NullableUnitGenerator;


/// <summary>
/// UnitOfOpenApiDataType属性を元に、SwaggerGenOptions に OpenApiSchema を設定する。
/// </summary>
public static class NullableUnitGeneratorExtensions
{
    /// <summary>
    /// UnitOfOas 属性を探索し、SwaggerGenOptions に Schema 情報として登録する
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
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
    /// UnitOfOas 属性を元に、OpenApiSchema オブジェクトに変換する。
    /// </summary>
    /// <param name="attr"></param>
    /// <returns></returns>
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
