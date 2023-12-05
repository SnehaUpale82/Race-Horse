using System.Data;

namespace Infrastructure.Dapper.Interface;

public interface IRacingDapperConnectionFactory
{
    IDbConnection Connection();
}