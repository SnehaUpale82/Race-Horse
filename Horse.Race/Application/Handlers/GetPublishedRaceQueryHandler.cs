using Application.Queries;
using Application.Responses;
using Domain.Interface;
using MediatR;

namespace Application.Handlers;

public class GetPublishedRaceQueryHandler : IRequestHandler<GetPublishedRaceQuery, GetPublishedRaceResponse>
{
    private readonly IDbRaceRepository _dbRepository;

    public GetPublishedRaceQueryHandler(IDbRaceRepository raceRepository)
    {
        _dbRepository = raceRepository;
    }


    public async Task<GetPublishedRaceResponse> Handle(GetPublishedRaceQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbRepository.GetAllRacesAsync(cancellationToken);

        return new GetPublishedRaceResponse()
        {
            Races = result
        };
    }
}