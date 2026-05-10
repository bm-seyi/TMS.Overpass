using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TMS.Overpass.Application.Interfaces.Infrastructure.Persistence;
using TMS.Overpass.Infrastructure.Interfaces;

namespace TMS.Overpass.Infrastructure.Persistence;

internal sealed class UnitOfWork<T> : IAsyncDisposable, IUnitOfWork where T : notnull, IDapperContext
{
    private readonly T _context;
    private readonly ILogger<UnitOfWork<T>> _logger;

    private static readonly ActivitySource _activitySource = new("TMS.Overpass.Infrastructure");

    public UnitOfWork(T context, ILogger<UnitOfWork<T>> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public T Context => _context;

    public async Task OpenAsync(CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("UnitOfWork.OpenAsync");
        await _context.OpenAsync(cancellationToken);
    }

    public async Task BeginAsync(CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("UnitOfWork.BeginAsync");
        await _context.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        using Activity? _ = _activitySource.StartActivity("UnitOfWork.SaveChangesAsync");

        try
        {
            _logger.LogDebug("Committing transaction");
            await _context.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during SaveChangesAsync, rolling back");
            await _context.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}