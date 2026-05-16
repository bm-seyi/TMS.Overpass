using System.Text.Json.Serialization;

namespace TMS.Overpass.Domain.Overpass;

public sealed record Node
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("lat")]
    public required double Latitude { get; init; }
    
    [JsonPropertyName("lon")]
    public required double Longitude { get; init; }

    [JsonPropertyName("tags")]
    public Tags? Tags { get; init; }
}