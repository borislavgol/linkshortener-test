using AutoMapper;
using LinkShortener.Dal.Entities;
using LinkShortener.Models;

namespace LinkShortener.Dal.Mapper
{
    internal class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(x => x.Balance, e => e.MapFrom(e => e.Balance.Balance));

            CreateMap<ShortedLinkEntity, ShortedLinkModel>()
                .ForMember(x => x.ShortedLink, p => p.MapFrom(e => e.ShortLink));
        }
    }
}
