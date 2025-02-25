using AutoMapper;

namespace Lego.Api.Profiles
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<Entities.Part, Models.PartDto>();
        }
    }
}
