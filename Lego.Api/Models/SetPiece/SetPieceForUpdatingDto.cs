using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.SetPiece
{
    /// <summary>
    /// A piece for updating of a Lego set 
    /// </summary>
    public class SetPieceForUpdatingDto
    {
        /// <summary>
        /// The quantity of the type of piece of a Lego set
        /// </summary>
        [Range(1, Int32.MaxValue)]
        public int? Quantity { get; set; }
    }
}
