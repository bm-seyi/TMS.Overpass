using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence.Repositories;
using TMS.Overpass.Domain.DTOs;
using TMS.Overpass.Infrastructure.Extensions;
using TMS.Overpass.Infrastructure.Interfaces;

namespace TMS.Overpass.Infrastructure.Persistence.Repositories;

internal sealed class LinesRepository(ILogger<LinesRepository> logger, [FromKeyedServices("TmsContext")]IDapperContext dbContext) : ILinesRepository
{
    private readonly ILogger<LinesRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDapperContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private static readonly ActivitySource _activitySource = new ActivitySource("TMS.Overpass.Infrastructure");

    public async Task BulkAsync(IEnumerable<LinesDTO> linesDTOs, CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("LinesRepository.BulkAsync");

        DataTable dataTable = linesDTOs.ToDataTable();

        using SqlBulkCopy bulk = new SqlBulkCopy(_dbContext.Connection,SqlBulkCopyOptions.TableLock, (SqlTransaction?)_dbContext.Transaction);

        bulk.DestinationTableName = "#temp";
        bulk.BatchSize = 5000;

        await bulk.WriteToServerAsync(dataTable, cancellationToken);
    }
}