namespace TMS.Overpass.Application.Interfaces.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task OpenAsync(CancellationToken cancellationToken);
    Task BeginAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}