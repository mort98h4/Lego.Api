using AutoMapper;
using Lego.Api.Models.Piece;

namespace Lego.Api.Profiles
{
    public class PieceProfile : Profile
    {
        public PieceProfile()
        {
            CreateMap<Entities.Piece, PieceDto>();
            CreateMap<PieceForCreationDto, Entities.Piece>();
            CreateMap<PieceForUpdatingDto, Entities.Piece>();
        }
    }
}
