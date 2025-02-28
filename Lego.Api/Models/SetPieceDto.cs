namespace Lego.Api.Models
{
    /// <summary>
    /// A piece of a Lego set
    /// </summary>
    public class SetMissingPieceIdDto
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
