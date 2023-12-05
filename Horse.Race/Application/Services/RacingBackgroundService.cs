using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class RacingBackgroundService : BackgroundService, IRacingBackgroundService
{
    private readonly ILogger<RacingBackgroundService> _logger;
    private readonly TimeSpan _sleepTime = TimeSpan.FromSeconds(30); 
    private readonly IServiceProvider _serviceProvider;

    public RacingBackgroundService(ILogger<RacingBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<IRacingService>();
                    await service.RunAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation token exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, _sleepTime);
                try
                {
                    await Task.Delay(_sleepTime, stoppingToken); // Don't want to get caught in a quick exception loop
                }
                catch (TaskCanceledException)
                {
                    // Ignore cancellation token exception
                }
            }
        }
    }
}