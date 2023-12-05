using Application.Queries;
using Application.Responses;
using AutoMapper;
using Domain.Interface;
using Domain.Models.DB.Entities;
using MediatR;

namespace Application.Handlers;

public class GetRaceQueryHandler : IRequestHandler<GetRaceQuery, GetRaceResponse>
{
    private readonly IRaceRepository _raceRepository;
    private readonly IMapper _mapper;

    public GetRaceQueryHandler(IRaceRepository raceRepository, IMapper mapper)
    {
        _raceRepository = raceRepository;
        _mapper = mapper;
    }


    public async Task<GetRaceResponse> Handle(GetRaceQuery request, CancellationToken cancellationToken)
    {
        var result = await _raceRepository.GetRacesAsync(cancellationToken);

        return new GetRaceResponse()
        {
            Races = result != null ? _mapper.Map<IEnumerable<Race>>(result) : new List<Race>()
        };
    }
}