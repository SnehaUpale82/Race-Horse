using System.Net;
using AutoFixture.Xunit2;
using Domain.Models.API.Entities;
using FluentAssertions;
using Infrastructure.Configuration;
using Infrastructure.Http;
using Infrastructure.Http.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;


namespace UnitTests.Infrastructure.Http;

    public class RaceApiClientTest
    {
        private readonly FakeHttpMessageHandler _fakeHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly RaceApiConfiguration _apiConfiguration;

        public RaceApiClientTest()
        {
            _fakeHttpMessageHandler = Substitute.ForPartsOf<FakeHttpMessageHandler>();
            _httpClient = new HttpClient(_fakeHttpMessageHandler);
            _apiConfiguration = new RaceApiConfiguration();
        }
        
        private IRaceApiClient GetClient()
        {
            var options = Substitute.For<IOptions<RaceApiConfiguration>>();
            options.Value.Returns(_apiConfiguration);
            return new RaceApiClient(_httpClient, options);
        }

        
        [Theory, AutoData]
        public async Task GetAsync_Returns_Ok_Content(IEnumerable<RaceUpdateDto> expectedItem)
        {
            // Arrange
            var content = JsonConvert.SerializeObject(expectedItem);
            _apiConfiguration.BaseUrl = "https://fake.com";
            var raceurl = $"api/race";

            var expectedRequestUri = new Uri(new Uri(_apiConfiguration.BaseUrl), raceurl).ToString();
        
            _fakeHttpMessageHandler
                .SendAsyncHandler(
                    Arg.Is<HttpRequestMessage>(request =>
                        request.RequestUri.Equals(expectedRequestUri) && request.Method == HttpMethod.Get),
                    Arg.Any<CancellationToken>())
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content)
                });
        
            // Act
            var result = await GetClient().GetAsync<IEnumerable<RaceUpdateDto>?>(expectedRequestUri, default);
        
            // Assert
            result.Should().BeEquivalentTo(expectedItem);
        }
        
        
    }