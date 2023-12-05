using System.Data;
using Infrastructure.Dapper.Interface;

namespace Infrastructure.Repositories;

public abstract class DapperRepository
{
    private static IRacingDapperConnectionFactory Factory { get; set; }
    protected readonly IDapperWrapper DapperWrapper;

    protected DapperRepository(IRacingDapperConnectionFactory factory, IDapperWrapper dapperWrapper)
    {
        Factory = factory;
        DapperWrapper = dapperWrapper;
    }

    protected static IDbConnection GetConnection()
    {
        return Factory.Connection();
    }
}