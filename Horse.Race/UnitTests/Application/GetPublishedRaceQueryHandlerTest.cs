using Application.Handlers;
using Application.Queries;
using AutoFixture.Xunit2;
using Domain.Interface;
using Domain.Models.DB.Entities;
using FluentAssertions;
using NSubstitute;


namespace UnitTests.Application;

public class GetPublishedRaceQueryHandlerTest
{
    private readonly IDbRaceRepository _dbRaceRepository;

    public GetPublishedRaceQueryHandlerTest()
    {
        _dbRaceRepository =  Substitute.For<IDbRaceRepository>();
    }

    private GetPublishedRaceQueryHandler GetHandler()
    {
        return new GetPublishedRaceQueryHandler(_dbRaceRepository);
    }

    [Theory, AutoData]
    public async Task GetPublishedRaceQueryHandler_Returns_Races_From_DB(IEnumerable<Race> expectedResponse)
    {
        //Arrange
        _dbRaceRepository.GetAllRacesAsync(Arg.Any<CancellationToken>()).Returns(expectedResponse);

        //Act
        var response = await GetHandler().Handle(new GetPublishedRaceQuery(), default);

        //Assert
        await _dbRaceRepository.Received(1).GetAllRacesAsync(default);
        response.Races.Should().BeEquivalentTo(expectedResponse);

    }
}