using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TMS.Overpass.Application.Interfaces.Application;
using TMS.Overpass.Application.Interfaces.Infrastructure.Http.Clients;
using TMS.Overpass.Application.Mapping;
using TMS.Overpass.Domain.DTOs;
using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Application.EtlPipelines;

internal sealed class LinesPipeline(ILogger<LinesPipeline> logger, IOpenStreetMapsClient openStreetMapsClient) : IEtlPipeline
{
    private readonly ILogger<LinesPipeline> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IOpenStreetMapsClient _openStreetMapsClient = openStreetMapsClient ?? throw new ArgumentNullException(nameof(openStreetMapsClient));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.Overpass.Application");

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("LinesPipeline.ExecuteAsync");

        string line = LinesOverpassQueryBuilder.Build("APL");
        
        OverpassResponse overpassResponse = await _openStreetMapsClient.RunQueryAsync(line, cancellationToken);
        OverpassMapper overpassMapper = new OverpassMapper();

        IEnumerable<LinesDTO> linesDTOs = overpassMapper.Map(overpassResponse, "AIR");

        



    }
}