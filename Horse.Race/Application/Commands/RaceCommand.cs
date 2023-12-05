using Application.Responses;
using Domain.Models.DB.Entities;
using MediatR;

namespace Application.Commands;

public class RaceCommand: IRequest<bool>
{
    public RaceCommand(IEnumerable<Race> races)
    {
        Races = races;
    }

    public IEnumerable<Race> Races { get; set; }
}