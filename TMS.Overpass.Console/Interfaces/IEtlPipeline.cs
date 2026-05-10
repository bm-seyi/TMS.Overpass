namespace TMS.Overpass.Console.Interfaces;

public interface IEtlPipeline
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}