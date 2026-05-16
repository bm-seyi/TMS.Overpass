using System.Text.Json.Serialization;

namespace TMS.Overpass.Domain.Overpass;

public sealed record Tags
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("network")]
    public string? Network { get; init; }

    [JsonPropertyName("network:wikidata")]
    public string? NetworkWikidata { get; init; }

    [JsonPropertyName("operator")]
    public string? Operator { get; init; }

    [JsonPropertyName("operator:wikidata")]
    public string? OperatorWikidata { get; init; }

    [JsonPropertyName("public_transport")]
    public string? PublicTransport { get; init; }

    [JsonPropertyName("railway")]
    public string? Railway { get; init; }

    [JsonPropertyName("tram")]
    public string? Tram { get; init; }

    [JsonPropertyName("zone")]
    public string? Zone { get; init; }

    [JsonPropertyName("electrified")]
    public string? Electrified { get; init; }

    [JsonPropertyName("frequency")]
    public string? Frequency  { get; init; }

    [JsonPropertyName("gauge")]
    public string? Gauge { get; init; }

}