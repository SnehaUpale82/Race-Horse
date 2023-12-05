using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Configuration;
using Infrastructure.Dapper.Interface;

namespace Infrastructure.Dapper;

public class RacingDapperConnectionFactory : IRacingDapperConnectionFactory
{
    private readonly string _connectionString;

    public RacingDapperConnectionFactory(IOptions<DatabaseConfiguration> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public IDbConnection Connection()
    {
        return new SqlConnection(_connectionString);
    }
}