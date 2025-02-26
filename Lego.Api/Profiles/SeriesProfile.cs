using AutoMapper;

namespace Lego.Api.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Entities.Collection, Models.SeriesDto>();
            CreateMap<Models.SeriesDto, Entities.Collection>();
        }
    }
}
