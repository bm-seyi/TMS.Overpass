using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TMS.Overpass.Console.Interfaces;

namespace TMS.Overpass.Console;

internal sealed class App(ILogger<App> logger, IEnumerable<IEtlPipeline> etlPipelines)
{
    private readonly ILogger<App> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEnumerable<IEtlPipeline> _etlPipelines = etlPipelines ?? throw new ArgumentNullException(nameof(etlPipelines));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.OverpassLines.Console");

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        using Activity? activity = _activitySource.StartActivity("App.RunAsync");

        ParallelOptions options = new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        await Parallel.ForEachAsync(_etlPipelines, options,
            async (pipeline, ct) =>
            {
                _logger.LogInformation("Starting pipeline {Pipeline}", pipeline.GetType().Name);

                await pipeline.ExecuteAsync(ct);

                _logger.LogInformation("Completed pipeline {Pipeline}", pipeline.GetType().Name);
            });
    }
}