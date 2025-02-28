using AutoMapper;
using Lego.Api.Models.Set;

namespace Lego.Api.Profiles
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<Entities.Set, SetDto>();
            CreateMap<Entities.Set, SetWithMissingPiecesDto>();
            CreateMap<SetForCreationDto, Entities.Set>();
            CreateMap<SetForUpdatingDto, Entities.Set>();
        }
    }
}
