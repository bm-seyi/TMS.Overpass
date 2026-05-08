using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TMS.OverpassLines.Application.Interfaces.Infrastructure.Http.Clients;
using TMS.OverpassLines.Domain.Overpass;
using TMS.OverpassLines.Infrastructure.Extensions;

namespace TMS.OverpassLines.Infrastructure.Http.Clients;

internal sealed class OpenStreetMapsClient(ILogger<OpenStreetMapsClient> logger, HttpClient httpClient) : IOpenStreetMapsClient
{
    private readonly ILogger<OpenStreetMapsClient> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.OpenStreetMapsLines.Infrastructure");

    public async Task<OverpassResponse> RunQueryAsync(string query, CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("OpenStreetMapsClient.RunQueryAsync");

        KeyValuePair<string, string> data = new KeyValuePair<string, string>("data", query);

        FormUrlEncodedContent stringContent = new FormUrlEncodedContent([data]);

        using HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("api/interpreter", stringContent, cancellationToken);

        OverpassResponse overpassResponse = await httpResponseMessage.Content.ReadRequiredFromJsonAsync<OverpassResponse>(cancellationToken);

        return overpassResponse;
    }
}