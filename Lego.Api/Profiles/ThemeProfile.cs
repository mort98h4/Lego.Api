using AutoMapper;

namespace Lego.Api.Profiles
{
    public class ThemeProfile : Profile
    {
        public ThemeProfile()
        {
            CreateMap<Entities.Theme, Models.ThemeDto>();
            CreateMap<Models.ThemeForCreationDto, Entities.Theme>();
        }
    }
}
