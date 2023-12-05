namespace Domain.Models.API.Enums;

public class APIEnums
{
    public enum PriceType
    {
        Win,
        Loss
    }

    public enum RaceType
    {
        Metropolitan,
        Metropolitan2
    }

    public enum TrackConditionType : ushort
    {
        VeryGood = 1,
        VeryGood1 = 2,
        VeryGood2 = 3,
        Good = 4,
    }
}