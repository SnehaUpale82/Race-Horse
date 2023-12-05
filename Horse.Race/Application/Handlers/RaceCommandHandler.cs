using Application.Commands;
using Domain.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class RaceCommandHandler : IRequestHandler<RaceCommand, bool>
{
    private readonly IDbRaceRepository _raceDbRepository;
    private readonly ILogger _logger;

    public RaceCommandHandler(IDbRaceRepository raceRepository, ILogger logger)
    {
        _raceDbRepository = raceRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(RaceCommand request, CancellationToken cancellationToken)
    {
        if (!request.Races.Any())
        {
            _logger.LogDebug("No race found to update.");
            return false;
        }

        foreach (var apiRace in request.Races)
        {
            var runnersApiList = new List<int>();
            var runnersDbList = new List<int>();

            var dbRace = await _raceDbRepository.GetRaceAsync(apiRace.RaceId, cancellationToken);

            if (dbRace != null)
            {
                _logger.LogDebug("Found a race in local DB.{race}", dbRace);

                if (apiRace.Runners.Any())
                {
                    runnersApiList = apiRace.Runners.Select(x => x.Id).ToList();
                }

                if (dbRace.Runners.Any())
                {
                    runnersDbList = dbRace.Runners.Select(x => x.Id).ToList();
                }

                // Find records in runnersDBList that do not exist in runnersAPIList - that needs to be deleted.
                var runnersTobeDeleted = runnersDbList.Except(runnersApiList).ToList();

                if ( !Equals(dbRace.TrackCondition, apiRace.TrackCondition))
                {
                    _logger.LogDebug(
                        "Track Condition has been changed due to weather for a race {raceId} from {oldTrackCondition} => {updatedTrackCondition}"
                        , apiRace.RaceId, dbRace.TrackCondition, apiRace.TrackCondition);

                    await _raceDbRepository.UpdateTrackConditionForRaceAsync(apiRace.RaceId, apiRace.TrackCondition, cancellationToken);
                }

                if (DateTimeOffset.Compare(dbRace.StartTimeUtc, apiRace.StartTimeUtc) != 0)
                {
                    _logger.LogDebug("Start Time has been changed for a race {raceId} from {oldStartTime} => {updatedStartTime}", apiRace.RaceId
                        , dbRace.StartTimeUtc, apiRace.StartTimeUtc);

                    await _raceDbRepository.UpdateStartTimeForRaceAsync(apiRace.StartTimeUtc, apiRace.RaceId, cancellationToken);
                }

                if (runnersTobeDeleted.Any())
                {
                    _logger.LogDebug("{runners} Runners pulled out due to injury from a race {raceId}", runnersTobeDeleted, apiRace.RaceId);

                    await _raceDbRepository.DeleteRunnersFromRacesAsync( apiRace.RaceId, runnersTobeDeleted, cancellationToken);
                }
            }
            else
            {
                _logger.LogDebug("new race added- {race}", apiRace);

                await _raceDbRepository.InsertRaceAsync(apiRace, cancellationToken);
            }
        }

        return true;
    }
}