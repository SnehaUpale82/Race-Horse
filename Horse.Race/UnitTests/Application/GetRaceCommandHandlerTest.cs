using Application.Commands;
using Application.Handlers;
using AutoFixture.Xunit2;
using Domain.Interface;
using Domain.Models.DB.Entities;
using Domain.Models.DB.Enums;
using Microsoft.Extensions.Logging;
using NSubstitute;


namespace UnitTests.Application;

public class GetRaceCommandHandlerTest
{
    private readonly IDbRaceRepository _raceDbRepository;
    private readonly ILogger _logger;

    public GetRaceCommandHandlerTest()
    {
        _raceDbRepository =  Substitute.For<IDbRaceRepository>();
        _logger = Substitute.For<ILogger>();
    }

    private RaceCommandHandler GetHandler()
    {
        return new RaceCommandHandler(_raceDbRepository, _logger);
    }

    [Theory, AutoData]
    public async Task GetRaceCommandHandler_Returns_UpdateRaces_For_TrackConditionChanges( int raceId, Race race)
    {
        //Arrange
        race.TrackCondition = DBEnums.TrackConditionType.Good;
        var racenew = new Race()
        {
            RaceId = race.RaceId, RaceInfo = race.RaceInfo, RaceLocation = race.RaceLocation,
            TrackCondition = DBEnums.TrackConditionType.Bad, StartTimeUtc = race.StartTimeUtc,
            Runners = race.Runners
        };

        IEnumerable<Race> races = new List<Race>()
        {
            racenew
        };

        var request = new RaceCommand(races);

        _raceDbRepository.GetRaceAsync(race.RaceId, Arg.Any<CancellationToken>()).Returns(race);
        _raceDbRepository.UpdateTrackConditionForRaceAsync(race.RaceId, Arg.Any<DBEnums.TrackConditionType>(), Arg.Any<CancellationToken>())
            .Returns(true);

        //Act
        var response = await GetHandler().Handle(request, default);

        //Assert
        await _raceDbRepository.Received(1).GetRaceAsync(race.RaceId, default);
        await _raceDbRepository.Received(1).UpdateTrackConditionForRaceAsync(race.RaceId, DBEnums.TrackConditionType.Bad, default);
        await _raceDbRepository.Received(0).UpdateStartTimeForRaceAsync( Arg.Any<DateTimeOffset>(),race.RaceId, default);
        await _raceDbRepository.Received(0).DeleteRunnersFromRacesAsync(race.RaceId, Arg.Any<List<int>>(), default);
        await _raceDbRepository.Received(0).InsertRaceAsync( Arg.Any<Race>(), default);
    }

    [Theory, AutoData]
    public async Task GetRaceCommandHandler_Returns_UpdateRaces_For_StartTimeChanges( int raceId, Race race)
    {
        //Arrange
        race.StartTimeUtc = new DateTimeOffset(
            new DateTime(2023, 5, 15, 7, 0, 0),
            new TimeSpan(-7, 0, 0)
        );
        var changedTime = new DateTimeOffset(
            new DateTime(2023, 5, 15, 8, 0, 0),
            new TimeSpan(-7, 0, 0)
        );

        var racenew = new Race()
        {
            RaceId = race.RaceId, RaceInfo = race.RaceInfo, RaceLocation = race.RaceLocation, TrackCondition = race.TrackCondition
            , StartTimeUtc = changedTime, Runners = race.Runners
        };

        IEnumerable<Race> races = new List<Race>()
        {
            racenew
        };

        var request = new RaceCommand(races);

        _raceDbRepository.GetRaceAsync(race.RaceId, Arg.Any<CancellationToken>()).Returns(race);
        _raceDbRepository.UpdateTrackConditionForRaceAsync(race.RaceId, Arg.Any<DBEnums.TrackConditionType>(), Arg.Any<CancellationToken>())
            .Returns(true);

        //Act
        var response = await GetHandler().Handle(request, default);

        //Assert
        await _raceDbRepository.Received(1).GetRaceAsync(race.RaceId, default);
        await _raceDbRepository.Received(0).UpdateTrackConditionForRaceAsync(race.RaceId, DBEnums.TrackConditionType.Bad, default);
        await _raceDbRepository.Received(1).UpdateStartTimeForRaceAsync( Arg.Any<DateTimeOffset>(),race.RaceId, default);
        await _raceDbRepository.Received(0).DeleteRunnersFromRacesAsync(race.RaceId, Arg.Any<List<int>>(), default);
        await _raceDbRepository.Received(0).InsertRaceAsync( Arg.Any<Race>(), default);
    }

}