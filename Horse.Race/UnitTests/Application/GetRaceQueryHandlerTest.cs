using Application.Handlers;
using Application.Queries;
using AutoFixture.Xunit2;
using AutoMapper;
using Domain.Interface;
using Domain.Models.API.Entities;
using Domain.Models.DB.Entities;
using FluentAssertions;
using NSubstitute;
using Mapper = Application.Mapper;

namespace UnitTests.Application;

public class GetRaceQueryHandlerTest
{
    private readonly IRaceRepository _raceRepository;
    private readonly IMapper _mapper;

    public GetRaceQueryHandlerTest()
    {
        _raceRepository =  Substitute.For<IRaceRepository>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Mapper.ConfigureServiceAutoMapper());
        });
        _mapper = mappingConfig.CreateMapper();
    }

    private GetRaceQueryHandler GetHandler()
    {
        return new GetRaceQueryHandler(_raceRepository, _mapper);
    }

    [Theory, AutoData]
    public async Task GetRaceQueryHandler_Returns_Races_From_HttpClient(IEnumerable<RaceUpdateDto> expectedResponse)
    {
        //Arrange
        _raceRepository.GetRacesAsync(Arg.Any<CancellationToken>()).Returns(expectedResponse);

        //Act
        var response = await GetHandler().Handle(new GetRaceQuery(), default);

        //Assert
        await _raceRepository.Received(1).GetRacesAsync(default);
        var expectedRaces = _mapper.Map<IEnumerable<Race>>(expectedResponse);
        response.Races.Should().BeEquivalentTo(expectedRaces);
    }
}