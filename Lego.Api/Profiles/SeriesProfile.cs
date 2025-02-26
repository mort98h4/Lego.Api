using AutoMapper;

namespace Lego.Api.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Entities.Series, Models.SeriesDto>();
            CreateMap<Models.SeriesDto, Entities.Series>();
        }
    }
}
