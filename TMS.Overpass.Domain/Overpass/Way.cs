using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace TMS.Overpass.Domain.Overpass;

public sealed class Way
{
    [JsonPropertyName("Type")]
    public required string Type { get; init; }

    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("nodes")]
    public required ImmutableArray<long> Nodes { get; init; }

    [JsonPropertyName("tags")]
    public required Tags Tags { get; init; }
}