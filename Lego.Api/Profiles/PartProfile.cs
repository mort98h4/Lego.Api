using AutoMapper;

namespace Lego.Api.Profiles
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<Entities.Piece, Models.PieceDto>();
        }
    }
}
