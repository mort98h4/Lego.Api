using AutoMapper;

namespace Lego.Api.Profiles
{
    public class SetPieceProfile : Profile
    {
        public SetPieceProfile()
        {
            CreateMap<Entities.SetPiece, Models.SetMissingPieceDto>();
            CreateMap<Entities.SetPiece, Models.SetMissingPieceIdDto>();
        }
    }
}
