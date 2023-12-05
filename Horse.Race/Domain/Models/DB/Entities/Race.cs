using System.ComponentModel;
using System.Text.Json.Serialization;
using Domain.Models.DB.Entities;
using Domain.Models.DB.Enums;

namespace Domain.Models.DB.Entities;

public class Race
{
    public int RaceId { get; set; }
    public string? RaceLocation { get; set; }
    public int Distance { get; set; }
    public DBEnums.RaceType RaceType { get; set; }
    public string? RaceInfo { get; set; }
    public DBEnums.TrackConditionType TrackCondition { get; set; }

    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset StartTimeUtc { get; set; }
    public List<Runner> Runners { get; set; }
}