using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace TMS.Overpass.Application;

internal sealed class LinesPipeline(ILogger<LinesPipeline> logger)
{
    private readonly ILogger<LinesPipeline> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.Overpass.Application");
}