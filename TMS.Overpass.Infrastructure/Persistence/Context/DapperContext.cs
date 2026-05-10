using System.Data.Common;
using Microsoft.Data.SqlClient;
using TMS.Overpass.Infrastructure.Interfaces;

namespace TMS.Overpass.Infrastructure.Persistence.Context;

public abstract class DapperContext : IDapperContext
{
    private readonly SqlConnection _connection;

    protected DbTransaction? Transaction { get; private set; }
    protected SqlConnection Connection => _connection;

    SqlConnection IDapperContext.Connection => Connection;

    DbTransaction? IDapperContext.Transaction => Transaction;

    public DapperContext(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public async Task OpenAsync(CancellationToken cancellationToken) => await _connection.OpenAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken) => Transaction = await _connection.BeginTransactionAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (Transaction is not null)
        {
            await Transaction.CommitAsync(cancellationToken);
            await Transaction.DisposeAsync();
            Transaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (Transaction is not null)
        {
            await Transaction.RollbackAsync(cancellationToken);
            await Transaction.DisposeAsync();
            Transaction = null;
        }
    }


    public async ValueTask DisposeAsync()
    {
        if (Transaction is not null)
            await Transaction.DisposeAsync();

        await _connection.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}