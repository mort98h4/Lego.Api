using AutoMapper;
using Lego.Api.Models.Series;

namespace Lego.Api.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Entities.Series, SeriesDto>();
            CreateMap<SeriesForCreationDto, Entities.Series>();
            CreateMap<SeriesForUpdatingDto, Entities.Series>();
        }
    }
}
