namespace Domain.Models.DB.Enums;

public class DBEnums
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
        Bad = 3,
        Good = 4,
    }

}