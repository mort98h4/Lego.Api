using AutoMapper;

namespace Lego.Api.Profiles
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<Entities.Set, Models.SetDto>();
            CreateMap<Models.SetForCreationDto, Entities.Set>();
            CreateMap<Models.SetForUpdatingDto, Entities.Set>();
        }
    }
}
