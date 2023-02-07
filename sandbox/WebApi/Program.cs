using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Model.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v3", new OpenApiInfo { Title = "API", Version = "v3" });
            c.MapType<VoInt>(() => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true });
            //c.MapType<VoDouble>(() => new OpenApiSchema { Type = "number", Format = "double", Nullable = true });
            //c.MapType<VoDecimal>(() => new OpenApiSchema { Type = "number", Format = "double", Nullable = true });
            //c.MapType<VoDatetime>(() => new OpenApiSchema { Type = "string", Format = "date", Nullable = true });
            //c.MapType<VoString>(() => new OpenApiSchema { Type = "string", Format = "string", Nullable = true });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v3/swagger.json", "API v3");
            });
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
