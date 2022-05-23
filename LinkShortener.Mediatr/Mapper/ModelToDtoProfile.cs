using AutoMapper;
using LinkShortener.Dtos;
using LinkShortener.Models;

namespace LinkShortener.Mediatr.Mapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<ShortedLinkModel, LinkShortenResultDto>()
                .ForMember(x => x.ResultLink, m => m.MapFrom(x => x.ShortedLink));

            CreateMap<ShortedLinkModel, ShortedLinkDto>();
        }
    }
}
