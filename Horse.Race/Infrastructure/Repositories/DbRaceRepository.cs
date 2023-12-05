using System.Data;
using Dapper;
using Domain.Interface;
using Domain.Models.DB.Entities;
using Domain.Models.DB.Enums;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interface;

namespace Infrastructure.Repositories;

public class DbRaceRepository : DapperRepository, IDbRaceRepository
{
    private readonly DapperCommand _getAllRaces =
        new("[dbo].[GetRaces]", CommandType.StoredProcedure);

    private readonly DapperCommand _getRaceById =
        new("[dbo].[GetRaceById]", CommandType.StoredProcedure);

    private readonly DapperCommand _deleteRunnerFromRaceId =
        new("[dbo].[DeleteRunnerFromRaceId]", CommandType.StoredProcedure);

    private readonly DapperCommand _updateTrackConditionFromRaceId =
        new("[dbo].[UpdateTrackConditionFromRaceId]", CommandType.StoredProcedure);

    private readonly DapperCommand _updateStartDateTimeFromRaceId =
        new("[dbo].[UpdateStartDateTimeFromRaceId]", CommandType.StoredProcedure);

    private readonly DapperCommand _insertRace =
        new("[dbo].[InsertRace]", CommandType.StoredProcedure);

    private readonly DapperCommand _insertRunners =
        new("[dbo].[InsertRunners]", CommandType.StoredProcedure);

    public DbRaceRepository(IRacingDapperConnectionFactory factory, IDapperWrapper dapperWrapper)
        : base(factory, dapperWrapper)
    {

    }

    public async Task<IEnumerable<Race>> GetAllRacesAsync( CancellationToken cancellationToken = default)
    {
        return await DapperWrapper.QueryAsync<Race>(GetConnection(), _getAllRaces);
    }

    public async Task<Race> GetRaceAsync(int raceId, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("raceId", raceId);
        return await DapperWrapper.QueryFirstOrDefaultAsync<Race>(GetConnection(), _getRaceById, parameters);
    }


    public async Task<bool> DeleteRunnersFromRacesAsync(int raceId, List<int> runnerIds, CancellationToken cancellationToken = default)
    {
        var ids = GetIds(runnerIds);

        var parameters = new DynamicParameters();
        parameters.Add("raceId", raceId);
        parameters.Add("runnerIds", ids.AsTableValuedParameter("IdDataType"));

        var result =  await DapperWrapper.ExecuteAsync(GetConnection(), _deleteRunnerFromRaceId, parameters);

        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateTrackConditionForRaceAsync(int raceId, DBEnums.TrackConditionType trackConditionTypeId, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("raceId", raceId);
        parameters.Add("trackConditionTypeId", trackConditionTypeId);

        var result =  await DapperWrapper.ExecuteAsync(GetConnection(), _updateTrackConditionFromRaceId, parameters);

        if (result > 0)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateStartTimeForRaceAsync(DateTimeOffset newStartDateTime, int raceId, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("raceId", raceId);
        parameters.Add("startDateTime", newStartDateTime);

        var result =  await DapperWrapper.ExecuteAsync(GetConnection(), _updateStartDateTimeFromRaceId, parameters);

        if (result > 0)
        {
            return true;
        }

        return false;
    }


    public async Task<bool> InsertRaceAsync(Race raceDetails, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("raceId", raceDetails.RaceId);
        parameters.Add("raceInfo", raceDetails.RaceInfo);
        parameters.Add("raceLocation", raceDetails.RaceLocation);
        parameters.Add("raceType", raceDetails.RaceType);
        parameters.Add("distance", raceDetails.Distance);
        parameters.Add("trackCondition", raceDetails.TrackCondition);
        parameters.Add("startTimeUTC", raceDetails.StartTimeUtc);

        var result =  await DapperWrapper.ExecuteAsync(GetConnection(), _insertRace, parameters);

        if (result > 0)
        {
            foreach (var r in  raceDetails.Runners)
            {
                r.RaceId = raceDetails.RaceId;
            }

            await InsertRunnersAsync(raceDetails.Runners, cancellationToken);

            return true;
        }

        return false;
    }

    public async Task<bool> InsertRunnersAsync(List<Runner> runners, CancellationToken cancellationToken = default)
    {
        var dt = GetRunnerDataTable(runners);

        var parameters = new DynamicParameters();
        parameters.Add("runner", dt.AsTableValuedParameter("RunnersDataType"));

        var result =  await DapperWrapper.ExecuteAsync(GetConnection(), _insertRunners, parameters);

        if (result > 0)
        {
            return true;
        }

        return false;
    }


    private DataTable GetRunnerDataTable(List<Runner> runners)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Number", typeof(int));
        table.Columns.Add("Barrier", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("WinPrice", typeof(decimal));
        table.Columns.Add("Jockey", typeof(string));
        table.Columns.Add("Trainer", typeof(string));
        table.Columns.Add("RaceId", typeof(int));

        foreach (var r in runners)
        {
            table.Rows.Add(r.Number, r.Number, r.Name, r.WinPrice, r.Jockey, r.Trainer, r.RaceId);
        }

        return table;
    }

    private DataTable GetIds(List<int> ids)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Id", typeof(int));

        foreach (var r in ids)
        {
            table.Rows.Add(r);
        }

        return table;
    }


}