using AutoMapper;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UpdateTopicRequestDto, Topic>()
                    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                        src.Location.City,
                        src.Location.Street)))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => dest.Id)).ReverseMap();

            CreateMap<CreateTopicRequestDto, Topic>()
                    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                        src.Location.City,
                        src.Location.Street)))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => TopicId.Of(Guid.NewGuid()))).ReverseMap();
        }
    }
}
