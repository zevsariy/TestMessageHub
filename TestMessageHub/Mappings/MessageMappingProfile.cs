using AutoMapper;
using TestMessageHub.Models;

namespace TestMessageHub.Mappings
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            //To Puma
            CreateMap<DBMessageEntity, PumaMessage>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Message));

            //To Adidas
            CreateMap<DBMessageEntity, AdidasMessage>()
                .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message));

            //To Nike
            CreateMap<DBMessageEntity, NikeMessage>()
                .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));

            //To DBMessageEntity
            CreateMap<AdidasMessage, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Header))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content));

            CreateMap<PumaMessage, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Body));

            CreateMap<NikeMessage, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Caption))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
