using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TMS.Overpass.Application.Interfaces.Application;
using TMS.Overpass.Application.Interfaces.Infrastructure.Http.Clients;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence.Repositories;
using TMS.Overpass.Application.Mapping;
using TMS.Overpass.Domain.DTOs;
using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Application.EtlPipelines;

internal sealed class LinesPipeline(ILogger<LinesPipeline> logger, IOpenStreetMapsClient openStreetMapsClient, [FromKeyedServices("TmsContext")]IUnitOfWork unitOfWork, ILinesRepository linesRepository) : IEtlPipeline
{
    private readonly ILogger<LinesPipeline> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IOpenStreetMapsClient _openStreetMapsClient = openStreetMapsClient ?? throw new ArgumentNullException(nameof(openStreetMapsClient));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ILinesRepository _linesRepository = linesRepository ?? throw new ArgumentNullException(nameof(linesRepository));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.Overpass.Application");

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("LinesPipeline.ExecuteAsync");

        string line = LinesOverpassQueryBuilder.Build("APL");
        
        OverpassResponse overpassResponse = await _openStreetMapsClient.RunQueryAsync(line, cancellationToken);
        OverpassMapper overpassMapper = new OverpassMapper();

        IEnumerable<LinesDTO> linesDTOs = overpassMapper.Map(overpassResponse, "AIR");

        await _unitOfWork.BeginAsync(cancellationToken);
        await _linesRepository.BulkAsync(linesDTOs, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}