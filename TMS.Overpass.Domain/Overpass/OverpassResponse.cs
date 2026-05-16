using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace TMS.Overpass.Domain.Overpass;

public sealed record OverpassResponse
{
    [JsonPropertyName("version")]
    public required decimal Version { get; init; }

    [JsonPropertyName("generator")]
    public required string Generator { get; init; }

    public required ImmutableArray<Node> Nodes { get; init; }
    public required ImmutableArray<Way> Ways { get; init; }

}