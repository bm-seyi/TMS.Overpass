using TMS.Overpass.Domain.Overpass;

namespace TMS.Overpass.Application.Interfaces.Infrastructure.Http.Clients;

public interface IOpenStreetMapsClient
{
    Task<OverpassResponse> RunQueryAsync(string query, CancellationToken cancellationToken);
}