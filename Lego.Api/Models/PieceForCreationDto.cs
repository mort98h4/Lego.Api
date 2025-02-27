using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego piece for creation
    /// </summary>
    public class PieceForCreationDto
    {
        /// <summary>
        /// The piece number of the piece
        /// </summary>
        [Required(ErrorMessage = "You must provide a piece number")]
        [MaxLength(20)]
        [RegularExpression("^[1-9a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Piece number may only contain alphanumeric characters, and if the first character is a digit it should be more than zero.")]
        public string PieceNo { get; set; } = string.Empty;

        /// <summary>
        /// The color of the piece
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// The description of the piece
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
