using AutoMapper;
using TestMessageHub.Models;
using TestMessageHub.Models.DTO;

namespace TestMessageHub.Mappings
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            //To Puma
            CreateMap<DBMessageEntity, PumaMessageDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Message));

            //To Adidas
            CreateMap<DBMessageEntity, AdidasMessageDTO>()
                .ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message));

            //To Nike
            CreateMap<DBMessageEntity, NikeMessageDTO>()
                .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));

            //To DBMessageEntity
            CreateMap<AdidasMessageDTO, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Header))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Content));

            CreateMap<PumaMessageDTO, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Body));

            CreateMap<NikeMessageDTO, DBMessageEntity>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Caption))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
