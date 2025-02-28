using Lego.Api.Entities;
using Lego.Api.Models.Piece;

namespace Lego.Api.Models.SetPiece
{
    /// <summary>
    /// A piece of a Lego set
    /// </summary>
    public class SetPieceWithPieceDto
    {
        /// <summary>
        /// The number of this type of piece
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The piece
        /// </summary>
        public PieceDto Piece { get; set; } = new PieceDto();
    }
}
