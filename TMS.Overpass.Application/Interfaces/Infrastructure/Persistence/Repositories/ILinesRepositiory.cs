using TMS.Overpass.Domain.DTOs;

namespace TMS.Overpass.Application.Interfaces.Infrastructure.Persistence.Repositories;

public interface ILinesRepository
{
    Task BulkAsync(IEnumerable<LinesDTO> linesDTOs, CancellationToken cancellationToken);
}