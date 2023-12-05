namespace Application.Services;

public interface IRacingService
{
    Task RunAsync(CancellationToken cancellationToken = default);
}