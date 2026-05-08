using TMS.OverpassLines.Domain.Overpass;

namespace TMS.OverpassLines.Application.Interfaces.Infrastructure.Http.Clients;

public interface IOpenStreetMapsClient
{
    Task<OverpassResponse> RunQueryAsync(string query, CancellationToken cancellationToken);
}