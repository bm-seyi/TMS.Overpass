using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Infrastructure.Json.Converters;

internal sealed class OverpassResponseConverter : JsonConverter<OverpassResponse>
{
    public override OverpassResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        decimal version = root.GetProperty("version").GetDecimal();
        string generator = root.GetProperty("generator").GetString()!;

        ImmutableArray<Node>.Builder nodes = ImmutableArray.CreateBuilder<Node>();
        ImmutableArray<Way>.Builder ways = ImmutableArray.CreateBuilder<Way>();

        foreach (JsonElement element in root.GetProperty("elements").EnumerateArray())
        {
            string? type = element.GetProperty("type").GetString();

            switch (type)
            {
                case "node":
                    nodes.Add(JsonSerializer.Deserialize<Node>(element.GetRawText(), options)!);
                    break;

                case "way":
                    ways.Add(JsonSerializer.Deserialize<Way>(element.GetRawText(), options)!);
                    break;

                default:
                    continue;
            }
        }

        return new OverpassResponse
        {
            Version = version,
            Generator = generator,
            Nodes = nodes.ToImmutable(),
            Ways = ways.ToImmutable()
        };
    }

    public override void Write(Utf8JsonWriter writer, OverpassResponse value, JsonSerializerOptions options)
    {
        throw new NotSupportedException("Serialization of OverpassResponse is not supported.");
    }
}