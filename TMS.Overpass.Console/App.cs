using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace TMS.OverpassLines.Console;

internal sealed class App(ILogger<App> logger)
{
    private readonly ILogger<App> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.OverpassLines.Console");

    public async Task RunAsync()
    {
        string[]lines = ["APL", "ATL", "ASL", "BRL", "CCL", "SCL", "ECL", "EDD", "EYL", "ORL", "TPL"];

        foreach (string line in lines)
        {
            var data = await extract.executeAsync(line);
            var modified = await transform.executeAsync(data)
            load.loadasync(moodified);
        }
    }
}