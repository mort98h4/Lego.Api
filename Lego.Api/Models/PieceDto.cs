namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego piece
    /// </summary>
    public class PieceDto
    {
        /// <summary>
        /// The id of the piece
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The part number of the piece
        /// </summary>
        public string PieceNo { get; set; } = string.Empty;

        /// <summary>
        /// The color of the piece
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// The description of the piece
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
