using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.SetPiece
{
    public class SetPieceForCreationDto
    {
        public int? SetId { get; set; }

        [Required]
        public int PieceId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
