using Lego.Api.Entities;

namespace Lego.Api.Models
{
    /// <summary>
    /// A missing piece of a Lego set
    /// </summary>
    public class SetMissingPieceDto
    {
        /// <summary>
        /// The id of the set
        /// </summary>
        public int SetId { get; set; }

        ///// <summary>
        ///// The id of the piece
        ///// </summary>
        //public int PieceId { get; set; }

        /// <summary>
        /// The piece that is missing
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        /// The number of this type of piece that is missing
        /// </summary>
        public int Quantity { get; set; }
    }
}
