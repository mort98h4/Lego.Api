using Lego.Api.Entities;

namespace Lego.Api.Models
{
    /// <summary>
    /// A missing piece of a Lego set
    /// </summary>
    public class SetMissingPieceDto
    {
        /// <summary>
        /// The number of this type of missing piece
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The missing piece
        /// </summary>
        public PieceDto Piece { get; set; } = new PieceDto();
    }
}
