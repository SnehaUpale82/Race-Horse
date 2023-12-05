using Domain.Models.DB.Entities;
using Domain.Models.DB.Enums;

namespace Domain.Interface;

public interface IDbRaceRepository
{
    /// <summary>
    /// Get all races.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of races</returns>
    Task<IEnumerable<Race>> GetAllRacesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a race for a given racedId.
    /// </summary>
    /// <param name="raceId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>race details</returns>
    Task<Race> GetRaceAsync(int raceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete runners for a given racedId.
    /// </summary>
    /// <param name="raceId"></param>
    /// <param name="runnerIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>true/false</returns>
    Task<bool> DeleteRunnersFromRacesAsync(int raceId, List<int> runnerIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update track condition for a given racedId.
    /// </summary>
    /// <param name="raceId"></param>
    /// <param name="trackConditionTypeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>true/false</returns>
    Task<bool> UpdateTrackConditionForRaceAsync(int raceId
        , DBEnums.TrackConditionType trackConditionTypeId
        , CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert new race.
    /// </summary>
    /// <param name="raceDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>true/false</returns>
    Task<bool> InsertRaceAsync(Race raceDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update start time for a given racedId.
    /// </summary>
    /// <param name="raceId"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="newStartDateTime"></param>
    /// <returns>true/false</returns>
    Task<bool> UpdateStartTimeForRaceAsync(DateTimeOffset newStartDateTime, int raceId, CancellationToken  cancellationToken = default);
}