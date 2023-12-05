using Application.Internal;
using AutoMapper;

namespace Application;

public class Mapper
{
    public class ConfigureServiceAutoMapper : Profile
    {
        public ConfigureServiceAutoMapper()
        {
            CreateMap<Domain.Models.API.Enums.APIEnums.PriceType,Domain.Models.DB.Enums.DBEnums.PriceType>();
            CreateMap<Domain.Models.API.Enums.APIEnums.RaceType, Domain.Models.DB.Enums.DBEnums.RaceType>();
            CreateMap<Domain.Models.API.Enums.APIEnums.TrackConditionType, Domain.Models.DB.Enums.DBEnums.TrackConditionType>().ReverseMap();

            CreateMap<Domain.Models.API.Entities.RunnerDto, Domain.Models.DB.Entities.Runner>()
                .ForMember(x => x.WinPrice, s => s.MapFrom(s => s.Price))
                .ForMember(x => x.Number, s => s.MapFrom(s => s.TabNo)).ReverseMap();


            CreateMap<Domain.Models.API.Entities.RaceUpdateDto, Domain.Models.DB.Entities.Race>()
                .ForMember(x => x.Distance, s => s.MapFrom(s => s.RaceDistance))
                .ForMember(dest => dest.StartTimeUtc, opt => opt.MapFrom<IntToDateTimeOffsetConverter>());

            CreateMap<Domain.Models.DB.Entities.Race, Domain.Models.API.Entities.RaceUpdateDto>()
                .ForMember(x => x.RaceDistance, s => s.MapFrom(s => s.Distance))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom<DateTimeOffsetToLongConverter>());


        }

    }

}