using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Dapper.Interface;

public interface IDapperWrapper
{

    Task<IEnumerable<T>> QueryAsync<T>(IDbConnection dbConnection, DapperCommand dapperCommand);

    Task<int> ExecuteAsync(IDbConnection dbConnection, DapperCommand dapperCommand, DynamicParameters
       parameters);

    Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection dbConnection, DapperCommand dapperCommand);

    Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection dbConnection, DapperCommand dapperCommand, DynamicParameters parameters);

}