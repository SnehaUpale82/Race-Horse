using Application.Responses;
using MediatR;

namespace Application.Queries;

public record GetPublishedRaceQuery : IRequest<GetPublishedRaceResponse>;