using System.IO;
using System.Reflection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NullableUnitGenerator;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WebApiApp;

/// <summary></summary>
public class Program
{
    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddConfig(builder.Configuration);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NullableUnitOfAPI", Version = "v1" });
            c.MapTypeUnitOfOas();
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();

        //app.MapGet("/api", (HttpContext ctx, LinkGenerator link)
        //    => $"API1: PathBase: {ctx.Request.PathBase} Path: {ctx.Request.Path}");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options => { });
            //app.UseSwaggerUI();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

}




/// <summary>
/// Service への Config 設定用の拡張メソッド定義クラス
/// </summary>
public static class ConfigServiceCollectionExtensions
{
    /// <summary>
    /// <b>以下を実施するための拡張メソッド</b><br/>
    /// ・ appsettings.json に記述しているオプションを取得する<br/>
    /// ・ PathBase を Service への Config に設定する<br/>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        // Fetch the AppSetting section from configuration
        var c = config.GetSection(AppOptions.Section);

        // Bind the config section to AppSetting using IOptions
        services.Configure<AppOptions>(c);

        // Register the startup filter
        services.AddTransient<IStartupFilter, PathBaseStartupFilter>();

        return services;
    }
}


/// <summary>
/// PathBase を設定するためのクラス<br/>
/// (ConfigServiceCollectionExtensions.AddConfig にて使用)
/// </summary>
public class PathBaseStartupFilter : IStartupFilter
{
    private readonly AppOptions _AppSetting;

    /// <summary></summary>
    /// <param name="options"></param>
    public PathBaseStartupFilter(IOptions<AppOptions> options) => _AppSetting = options.Value;

    /// <summary></summary>
    /// <param name="next"></param>
    /// <returns></returns>
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UsePathBase(_AppSetting.PathBase);
            next(app);
        };
    }
}
