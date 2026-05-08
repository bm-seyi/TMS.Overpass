namespace TMS.Overpass.Application.Interfaces.Application;

public interface IEtlPipeline
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}