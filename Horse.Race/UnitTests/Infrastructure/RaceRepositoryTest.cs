using AutoFixture.Xunit2;
using Domain.Interface;
using Domain.Models.API.Entities;
using FluentAssertions;
using Infrastructure.Http.Interface;
using Infrastructure.Repositories;
using NSubstitute;

namespace UnitTests.Infrastructure;

public class RaceRepositoryTest
{
    private readonly IRaceApiClient _apiClient;

    public RaceRepositoryTest()
    {
        _apiClient = Substitute.For<IRaceApiClient>();
    }

    private IRaceRepository GetRepository()
    {
        return new RaceRepository(_apiClient);
    }


    [Theory, AutoData]
    public async Task GetRacesAsync_Returns_Races_From_HttpClient(IEnumerable<RaceUpdateDto> expectedResponse)
    {
        // Arrange
        var url = "/api/race";

         _apiClient
            .GetAsync<IEnumerable<RaceUpdateDto>>(url)
            .Returns(expectedResponse);

        var repository = GetRepository();

        // Act
        var actualContent = await repository.GetRacesAsync(default);

        // Assert
        actualContent.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetRacesAsync_Returns_Null_From_HttpClient()
    {
        // Arrange
        IEnumerable<RaceUpdateDto>? list = null;
        var url = "/api/race";

         _apiClient
            .GetAsync<IEnumerable<RaceUpdateDto>>(url)
            .Returns(list);

        var repository = GetRepository();

        // Act
        var actualContent = await repository.GetRacesAsync(default);

        // Assert
        actualContent.Should().BeNull();
    }


}