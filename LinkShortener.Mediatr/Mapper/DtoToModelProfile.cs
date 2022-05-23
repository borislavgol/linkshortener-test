using AutoMapper;
using LinkShortener.Dtos;
using LinkShortener.Models;

namespace LinkShortener.Mediatr.Mapper
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<LinkShortenRequestDto, ShortenLinkModel>();
        }
    }
}
