using AutoMapper;

namespace Lego.Api.Profiles
{
    public class PieceProfile : Profile
    {
        public PieceProfile()
        {
            CreateMap<Entities.Piece, Models.PieceDto>();
            CreateMap<Models.PieceForCreationDto, Entities.Piece>();
            CreateMap<Models.PieceForUpdatingDto, Entities.Piece>();
        }
    }
}
