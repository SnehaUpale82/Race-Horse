using Domain.Helper;
using Domain.Models.API.Entities;
using Infrastructure.Configuration;
using Infrastructure.Http.Interface;
using Microsoft.Extensions.Options;


namespace Infrastructure.Http;

public class RaceApiClient : IRaceApiClient
{
    private readonly HttpClient _httpClient;

    public RaceApiClient(HttpClient httpClient, IOptions<RaceApiConfiguration> options)
    {
        var configuration = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration.BaseUrl);
    }

    public async Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, path);

        var result = await _httpClient.SendAsync(request, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            var errorContent = await result.Content.ReadAsStringAsync(cancellationToken);

            throw new ApiClientException
            {
                TheStatusCode = result.StatusCode,
                ThePath = request.RequestUri?.ToString(),
                TheType = typeof(T),
                TheErrorContent = errorContent
            };
        }

        return await result.ReadContentAsAsync<T>(cancellationToken);
    }

}