using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class RacingService : IRacingService
{
    private readonly TimeSpan _sleepTime = TimeSpan.FromSeconds(900); // every 15 min

    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public RacingService(ILogger logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Started getting races at {date}", DateTime.UtcNow);
        CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        var timer = new PeriodicTimer(_sleepTime);

        try
        {
            do
            {
                var getRacesResponse = await _mediator.Send(new GetRaceQuery(), cancellationToken);

                _logger.LogDebug("Received races {races}", getRacesResponse);

                _logger.LogDebug("Started updating local published races.");

                 await _mediator.Send(new RaceCommand(getRacesResponse.Races), cancellationToken);

                _logger.LogDebug("Processing Race data finished.");

            } while (await timer.WaitForNextTickAsync(cancellationToken));

        }
        catch (OperationCanceledException)
        {
            // Ignore Task Cancelled
        }
    }

}