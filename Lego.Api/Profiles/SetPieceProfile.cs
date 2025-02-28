using AutoMapper;
using Lego.Api.Models.SetPiece;

namespace Lego.Api.Profiles
{
    public class SetPieceProfile : Profile
    {
        public SetPieceProfile()
        {
            CreateMap<Entities.SetPiece, SetPieceWithPieceDto>();
            CreateMap<Entities.SetPiece, SetPieceDto>();
            CreateMap<SetPieceForCreationDto, Entities.SetPiece>();
        }
    }
}
