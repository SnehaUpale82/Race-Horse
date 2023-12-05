namespace Infrastructure.Http.Interface;

public interface IRaceApiClient
{
    Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken = default);
}