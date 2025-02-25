using AutoMapper;

namespace Lego.Api.Profiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<Entities.Collection, Models.CollectionDto>();
            CreateMap<Models.CollectionDto, Entities.Collection>();
        }
    }
}
