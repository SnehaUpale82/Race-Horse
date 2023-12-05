using System.Data;
using AutoFixture.Xunit2;
using Dapper;
using Domain.Interface;
using Domain.Models.DB.Entities;
using Domain.Models.DB.Enums;
using FluentAssertions;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interface;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure;

public class DbRaceRepositoryTest
{

    private readonly IRacingDapperConnectionFactory _selfWealthDapperConnectionFactory;
    private readonly IDapperWrapper _dapperWrapper;

    public DbRaceRepositoryTest()
    {
        _selfWealthDapperConnectionFactory = Substitute.For<IRacingDapperConnectionFactory>();
        _dapperWrapper = Substitute.For<IDapperWrapper>();
    }

    private IDbRaceRepository GetRepository()
    {
        return new DbRaceRepository(_selfWealthDapperConnectionFactory, _dapperWrapper);
    }


    [Theory, AutoData]
    public async Task GetAllRacesAsyncTest(IEnumerable<Race> races)
    {
        //Arrange
        _dapperWrapper.QueryAsync<Race>(
            Arg.Any<IDbConnection>(),
            Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[GetRaces]")).Returns(races);

        //Act
        var result = await GetRepository().GetAllRacesAsync(default);

        //Assert
        await _dapperWrapper.Received(1).QueryAsync<Race>(
            Arg.Any<IDbConnection>(),
            Arg.Is<DapperCommand>(x =>
                x.Command.ToString() == "[dbo].[GetRaces]"));

        result.Should().NotBeEmpty();
    }

    [Theory, AutoData]
    public async Task GetRaceAsyncTest(Race race, int raceId)
    {
        //Arrange
        _dapperWrapper.QueryFirstOrDefaultAsync<Race>(Arg.Any<IDbConnection>()
                , Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[GetRaceById]")
                    , Arg.Is<DynamicParameters>(x => x.Get<int>("raceId") == raceId))
            .Returns(race);

        //Act
        var result = await GetRepository().GetRaceAsync(raceId, default);

        //Assert
        await _dapperWrapper.Received(1).QueryFirstOrDefaultAsync<Race>(
            Arg.Any<IDbConnection>(),
            Arg.Is<DapperCommand>(x =>
                x.Command.ToString() == "[dbo].[GetRaceById]"),
                Arg.Is<DynamicParameters>(x => x.Get<int>("raceId") == raceId));

        result.Should().NotBeNull();
    }

    [Theory, AutoData]
    public async Task UpdateTrackConditionForRaceAsyncTest(DBEnums.TrackConditionType type, int raceId)
    {
        //Arrange
        _dapperWrapper.ExecuteAsync(Arg.Any<IDbConnection>()
                , Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[UpdateTrackConditionFromRaceId]")
                , Arg.Is<DynamicParameters>(x => x.Get<int>("raceId") == raceId && x.Get<DBEnums.TrackConditionType>("trackConditionTypeId") == type))
            .Returns(1);

        //Act
        var result = await GetRepository().UpdateTrackConditionForRaceAsync(raceId, type, default);

        //Assert
        await _dapperWrapper.Received(1)
            .ExecuteAsync(Arg.Any<IDbConnection>(), Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[UpdateTrackConditionFromRaceId]")
                , Arg.Is<DynamicParameters>(x =>
                    x.Get<int>("raceId") == raceId && x.Get<DBEnums.TrackConditionType>("trackConditionTypeId") == type));

        result.Should().Be(true);
    }

    [Theory, AutoData]
    public async Task UpdateStartTimeForRaceAsyncTest(DateTimeOffset startTime, int raceId)
    {
        //Arrange
        _dapperWrapper.ExecuteAsync(Arg.Any<IDbConnection>()
                , Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[UpdateStartDateTimeFromRaceId]")
                , Arg.Is<DynamicParameters>(x => x.Get<int>("raceId") == raceId && x.Get<DateTimeOffset>("startDateTime") == startTime))
            .Returns(1);

        //Act
        var result = await GetRepository().UpdateStartTimeForRaceAsync(startTime, raceId, default);

        //Assert
        await _dapperWrapper.Received(1)
            .ExecuteAsync(Arg.Any<IDbConnection>(), Arg.Is<DapperCommand>(x => x.Command.ToString() == "[dbo].[UpdateStartDateTimeFromRaceId]")
                , Arg.Is<DynamicParameters>(x =>
                    x.Get<int>("raceId") == raceId && x.Get<DateTimeOffset>("startDateTime") == startTime));

        result.Should().Be(true);
    }

}