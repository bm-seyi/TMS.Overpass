using System.Text.Json.Serialization;

namespace TMS.OverpassLines.Domain.Overpass;

public sealed record OverpassResponse
{
    [JsonPropertyName("version")]
    public required decimal Version { get; init; }

    [JsonPropertyName("generator")]
    public required string Generator { get; init; }

    [JsonPropertyName("elements")]
    public required List<Node> Elements { get; init; }
}