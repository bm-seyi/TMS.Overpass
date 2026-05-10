using TMS.Overpass.Domain.DTOs;

namespace TMS.Overpass.Application.Interfaces.Infrastructure.Persistence.Repositories;

public interface ILinesRepository
{
    Task StageLinesAsync(IEnumerable<LinesDTO> linesDTOs, CancellationToken cancellationToken);
    Task MergeStagedLinesAsync(CancellationToken cancellationToken);
}