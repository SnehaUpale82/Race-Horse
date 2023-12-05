using Application.Responses;
using MediatR;

namespace Application.Queries;

public record GetRaceQuery : IRequest<GetRaceResponse>;