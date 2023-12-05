using Domain.Interface;
using Domain.Models.API.Entities;
using Infrastructure.Http.Interface;

namespace Infrastructure.Repositories;

public class RaceRepository : IRaceRepository
{
    private readonly IRaceApiClient _apiClient;

    public RaceRepository(IRaceApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public Task<IEnumerable<RaceUpdateDto>?> GetRacesAsync(CancellationToken cancellationToken = default)
    {
        return _apiClient.GetAsync<IEnumerable<RaceUpdateDto>?>(
            $"/api/race", cancellationToken);
    }
}