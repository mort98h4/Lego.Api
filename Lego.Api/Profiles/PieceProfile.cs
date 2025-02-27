using AutoMapper;

namespace Lego.Api.Profiles
{
    public class PieceProfile : Profile
    {
        public PieceProfile()
        {
            CreateMap<Entities.Piece, Models.PieceDto>();
        }
    }
}
