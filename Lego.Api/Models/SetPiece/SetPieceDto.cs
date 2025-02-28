namespace Lego.Api.Models.SetPiece
{
    /// <summary>
    /// A piece of a Lego set
    /// </summary>
    public class SetPieceDto
    {
        /// <summary>
        /// The id of the piece
        /// </summary>
        public int PieceId { get; set; }

        /// <summary>
        /// The quantity of this type of piece
        /// </summary>
        public int Quantity { get; set; }
    }
}
