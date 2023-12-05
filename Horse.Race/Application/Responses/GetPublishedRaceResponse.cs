using System.Text.Json.Serialization;
using Application.Internal;
using Domain.Models.DB.Entities;

namespace Application.Responses;

public class GetPublishedRaceResponse
{
    public IEnumerable<Race> Races { get; set; }
}