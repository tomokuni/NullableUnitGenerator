using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace WebApiApp.Models.Base;


[JsonSerializable(typeof(VoInt),GenerationMode = JsonSourceGenerationMode.Metadata)]
internal partial class VoIntJsonContext : JsonSerializerContext
{
}

public class ValueObjectResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo typeInfo = base.GetTypeInfo(type, options);
        foreach (JsonPropertyInfo property in typeInfo.Properties.Reverse())
        {
            if (property.PropertyType.Name.ToLower() == "voint")
            {
                property.ShouldSerialize = static (obj, value) =>
                    !((dynamic)obj)["VInt"].IsUndefined;
            }
        }
        return typeInfo;
    }
}



/// <summary>JsonConverter</summary>
public class VoIntJsonConverter : JsonConverter<VoInt>, IJsonOnSerializing
{
    /// <summary>HandleNull</summary>
    public override bool HandleNull
        => true;

    public override bool CanConvert(Type typeToConvert)
    {
        return base.CanConvert(typeToConvert);
    }

    /// <summary>Write</summary>
    public override void Write(Utf8JsonWriter writer, VoInt value, JsonSerializerOptions options)
    {
        // undefined; value.IsUndefined の場合は key,value ともに書き込まない
        if (value.IsUndefined)
        {
            writer.Reset();
        }
        else if(value.IsNull)
        {
            //writer.WriteNull(nameof(VoInt));
            writer.WriteNullValue();
        }
        else
        {
            //writer.Reset();
            //writer.WriteNumber(nameof(VoInt), value.Value);
            var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
            if (converter is not null)
                converter.Write(writer, (int?)value.GetOrNull(), options);
            else
                throw new JsonException($"{typeof(int?)} converter does not found.");
        }
    }

    /// <summary>Read</summary>
    public override VoInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
        if (converter is not null)
        {
            var res = reader.TokenType switch
            {
                JsonTokenType.Null => VoInt.NullValue,
                JsonTokenType.None => VoInt.UndefinedValue,
                _ => new VoInt(converter.Read(ref reader, typeToConvert, options))
            };
            return res;
        }
        else
        {
            throw new JsonException($"{typeof(int?)} converter does not found.");
        }
    }

    /// <summary>WriteAsPropertyName</summary>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, VoInt value, JsonSerializerOptions options)
    {
        Span<byte> buffer = stackalloc byte[36];
        if (System.Buffers.Text.Utf8Formatter.TryFormat(value.m_value, buffer, out var written))
        {
            writer.WritePropertyName(buffer.Slice(0, written));
        }
        else
        {
            writer.WritePropertyName(value.m_value.ToString());
        }
    }

    /// <summary>ReadAsPropertyName</summary>
    public override VoInt ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (System.Buffers.Text.Utf8Parser.TryParse(reader.ValueSpan, out int value, out var consumed))
        {
            return new VoInt(value);
        }
        else
        {
            return new VoInt(int.Parse(reader.GetString()));
        }
    }

    public void OnSerializing()
    {
        throw new NotImplementedException();
    }
}



public class VoIntJsonConverter2 : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert == typeof(VoInt);

    public override JsonConverter CreateConverter(
        Type type,
        JsonSerializerOptions options)
    {
        //var options2 = new JsonSerializerOptions
        //{
        //    TypeInfoResolver = new DefaultJsonTypeInfoResolver
        //    {
        //        Modifiers = { IgnoreUndefinedValue }
        //    }
        //};
        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = { IgnoreUndefinedValue }
        };

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(VoIntJsonConverter),
            BindingFlags.CreateInstance,
            binder: null,
            args: new object[] { options },
            culture: null)!;

        return converter;
    }

    static void IgnoreUndefinedValue(JsonTypeInfo typeInfo)
    {
        if (typeInfo.Type != typeof(VoInt))
            return;

        foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
        {
            if (propertyInfo.PropertyType == typeof(int))
            {
                propertyInfo.ShouldSerialize = static (obj, value)
                    => !((VoInt)obj == VoInt.UndefinedValue);
            }
        }
    }

    /// <summary>JsonConverter</summary>
    public class VoIntJsonConverter : JsonConverter<VoInt>
    {
        /// <summary>HandleNull</summary>
        public override bool HandleNull
            => true;

        public override bool CanConvert(Type typeToConvert)
        {
            return base.CanConvert(typeToConvert);
        }

        /// <summary>Write</summary>
        public override void Write(Utf8JsonWriter writer, VoInt value, JsonSerializerOptions options)
        {
            // undefined; value.IsUndefined の場合は key,value ともに書き込まない
            if (value.IsUndefined)
            {
                writer.Reset();
            }
            else if (value.IsNull)
            {
                writer.Reset();
                writer.WriteNull(nameof(VoInt));
                writer.WriteNullValue();
            }
            else
            {
                //writer.Reset();
                //writer.WriteNumber(nameof(VoInt), value.Value);
                var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
                if (converter is not null)
                    converter.Write(writer, (int?)value.GetOrNull(), options);
                else
                    throw new JsonException($"{typeof(int?)} converter does not found.");
            }
        }

        /// <summary>Read</summary>
        public override VoInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
            if (converter is not null)
            {
                var res = reader.TokenType switch
                {
                    JsonTokenType.Null => VoInt.NullValue,
                    JsonTokenType.None => VoInt.UndefinedValue,
                    _ => new VoInt(converter.Read(ref reader, typeToConvert, options))
                };
                return res;
            }
            else
            {
                throw new JsonException($"{typeof(int?)} converter does not found.");
            }
        }

        /// <summary>WriteAsPropertyName</summary>
        public override void WriteAsPropertyName(Utf8JsonWriter writer, VoInt value, JsonSerializerOptions options)
        {
            Span<byte> buffer = stackalloc byte[36];
            if (System.Buffers.Text.Utf8Formatter.TryFormat(value.m_value, buffer, out var written))
            {
                writer.WritePropertyName(buffer.Slice(0, written));
            }
            else
            {
                writer.WritePropertyName(value.m_value.ToString());
            }
        }

        /// <summary>ReadAsPropertyName</summary>
        public override VoInt ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (System.Buffers.Text.Utf8Parser.TryParse(reader.ValueSpan, out int value, out var consumed))
            {
                return new VoInt(value);
            }
            else
            {
                return new VoInt(int.Parse(reader.GetString()));
            }
        }
    }
}
