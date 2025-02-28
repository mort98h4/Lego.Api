using AutoMapper;
using Lego.Api.Models.Theme;

namespace Lego.Api.Profiles
{
    public class ThemeProfile : Profile
    {
        public ThemeProfile()
        {
            CreateMap<Entities.Theme, ThemeDto>();
            CreateMap<ThemeForCreationDto, Entities.Theme>();
            CreateMap<ThemeForUpdatingDto, Entities.Theme>();
        }
    }
}
