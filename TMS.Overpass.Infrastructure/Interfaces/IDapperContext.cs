using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace TMS.Overpass.Infrastructure.Interfaces;

internal interface IDapperContext : IAsyncDisposable
{
    SqlConnection Connection { get; }
    DbTransaction? Transaction { get; }
    Task OpenAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}