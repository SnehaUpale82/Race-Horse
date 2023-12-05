using Application.Responses;
using AutoMapper;
using Domain.Models.API.Entities;

namespace Application.Internal;


public class IntToDateTimeOffsetConverter : IValueResolver<Domain.Models.API.Entities.RaceUpdateDto, Domain.Models.DB.Entities.Race, DateTimeOffset>
{
    public DateTimeOffset Resolve(Domain.Models.API.Entities.RaceUpdateDto source
        , Domain.Models.DB.Entities.Race destination
        , DateTimeOffset destMember
        , ResolutionContext context)
    {
        // Assuming 'IntProperty' is the property in the source class of type int
        long intValue = source.StartTime;

        // Convert int to DateTimeOffset utc
        DateTimeOffset dateTimeOffsetValue = DateTimeOffset.FromUnixTimeSeconds(intValue).ToUniversalTime();

        return dateTimeOffsetValue;
    }
}

public class DateTimeOffsetToLongConverter : IValueResolver<Domain.Models.DB.Entities.Race, Domain.Models.API.Entities.RaceUpdateDto,  long>
{
    public long Resolve(Domain.Models.DB.Entities.Race source
        , Domain.Models.API.Entities.RaceUpdateDto destination
        , long destMember
        , ResolutionContext context)
    {
        // Assuming 'IntProperty' is the property in the source class of type int
        DateTimeOffset dateTimeValue = source.StartTimeUtc;

        // Convert DateTimeOffset to long utc
        return dateTimeValue.Ticks;


    }
}