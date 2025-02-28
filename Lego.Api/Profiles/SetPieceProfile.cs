using AutoMapper;

namespace Lego.Api.Profiles
{
    public class SetPieceProfile : Profile
    {
        public SetPieceProfile()
        {
            CreateMap<Entities.SetPiece, Models.SetPieceWithPieceDto>();
            CreateMap<Entities.SetPiece, Models.SetPieceDto>();
            CreateMap<Models.SetPieceForCreationDto, Entities.SetPiece>();
        }
    }
}
