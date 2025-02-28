using Lego.Api.Entities;

namespace Lego.Api.Models
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
