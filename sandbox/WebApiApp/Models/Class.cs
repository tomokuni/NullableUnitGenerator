using System.Text.Json.Serialization;
using System.Text.Json;
using WebApi.Model.Base;
using System.Formats.Asn1;

namespace WebApiApp.Models;

/// <summary>JsonConverter</summary>
public class VoIntJsonConverter : JsonConverter
{
    /// <summary>HandleNull</summary>
    public override bool HandleNull
        => true;

    /// <summary>Write</summary>
    public override void WriteJson(JsonWriter writer, VoInt value, JsonSerializerOptions options)
    {
        // undefined; !value.IsDefined の場合は key,value ともに書き込まない
        if (!value.IsDefined)
        {

            writer.WriteUndefined();
            return;
        }

        var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
        if (converter is not null)
            converter.Write(writer, value.GetValueOrNull(), options);
        else
            throw new JsonException($"{typeof(int?)} converter does not found.");
    }

    /// <summary>Read</summary>
    public override VoInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = options.GetConverter(typeof(int?)) as JsonConverter<int?>;
        if (converter is not null)
        {
            var res = (reader.TokenType) switch
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

    ///// <summary>WriteAsPropertyName</summary>
    //public override void WriteAsPropertyName(Utf8JsonWriter writer, VoInt value, JsonSerializerOptions options)
    //{
    //    Span<byte> buffer = stackalloc byte[36];
    //    if (System.Buffers.Text.Utf8Formatter.TryFormat(value.m_value, buffer, out var written))
    //    {
    //        writer.WritePropertyName(buffer.Slice(0, written));
    //    }
    //    else
    //    {
    //        writer.WritePropertyName(value.m_value.ToString());
    //    }
    //}

    ///// <summary>ReadAsPropertyName</summary>
    //public override VoInt ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //{
    //    if (System.Buffers.Text.Utf8Parser.TryParse(reader.ValueSpan, out int value, out var consumed))
    //    {
    //        return new VoInt(value);
    //    }
    //    else
    //    {
    //        return new VoInt(int.Parse(reader.GetString()));
    //    }
    //}
}

