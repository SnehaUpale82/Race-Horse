using Domain.Models.API.Enums;

namespace Domain.Models.API.Entities;

public class RaceUpdateDto
{
    public int MeetingId { get; set; }
    public int RaceId { get; set; }
    public string? RaceLocation { get; set; }
    public int RaceDistance { get; set; }
    public int RaceNo { get; set; }
    public APIEnums.RaceType RaceType { get; set; }
    public string? RaceInfo { get; set; }
    public APIEnums.TrackConditionType TrackCondition { get; set; }
    public string? Source { get; set; }
    public APIEnums.PriceType PriceType { get; set; }
    public int PoolSize { get; set; }
    public long StartTime { get; set; }
    public int CreationTime { get; set; }

    public List<RunnerDto> Runners { get; set; }
}

public class RunnerDto
{
    public int Id { get; set; }
    public int TabNo { get; set; }
    public int Barrier { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Jockey { get; set; }
    public string? Trainer { get; set; }

}