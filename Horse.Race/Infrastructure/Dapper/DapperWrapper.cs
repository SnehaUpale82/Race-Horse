using Dapper;
using System.Data;
using Infrastructure.Dapper.Interface;
using static Dapper.SqlMapper;

namespace Infrastructure.Dapper;

public class DapperWrapper : IDapperWrapper
{
    public async Task<IEnumerable<T>> QueryAsync<T>(
        IDbConnection dbConnection,
        DapperCommand dapperCommand,
        DynamicParameters parameters
    )
    {
        using var connection = dbConnection;
        return await connection.QueryAsync<T>(dapperCommand.Definition(parameters));
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(
        IDbConnection dbConnection,
        DapperCommand dapperCommand
    )
    {
        using var connection = dbConnection;
        return await connection.QueryAsync<T>(dapperCommand.Definition());
    }

    public async Task<T> QuerySingleAsync<T>(
        IDbConnection dbConnection,
        DapperCommand dapperCommand,
        DynamicParameters parameters
    )
    {
        using var connection = dbConnection;
        return await connection.QuerySingleAsync<T>(dapperCommand.Definition(parameters));
    }

    public async Task<T> QuerySingleAsync<T>(
        IDbConnection dbConnection,
        DapperCommand dapperCommand
    )
    {
        using var connection = dbConnection;
        return await connection.QuerySingleAsync<T>(dapperCommand.Definition());
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection dbConnection, DapperCommand dapperCommand)
    {
        using var connection = dbConnection;
        return await connection.QueryFirstOrDefaultAsync<T>(dapperCommand.Definition());
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(
        IDbConnection dbConnection,
        DapperCommand dapperCommand,
        DynamicParameters parameters
    )
    {
        using var connection = dbConnection;
        return await connection.QueryFirstOrDefaultAsync<T>(dapperCommand.Definition(parameters));
    }

    public async Task<int> ExecuteAsync(IDbConnection dbConnection, DapperCommand dapperCommand, DynamicParameters
        parameters)
    {
        using var connection = dbConnection;
        return await connection.ExecuteAsync(dapperCommand.Definition(parameters));
    }
}