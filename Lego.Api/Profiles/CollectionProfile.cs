using AutoMapper;

namespace Lego.Api.Profiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<Entities.Collection, Models.SeriesDto>();
            CreateMap<Models.SeriesDto, Entities.Collection>();
        }
    }
}
