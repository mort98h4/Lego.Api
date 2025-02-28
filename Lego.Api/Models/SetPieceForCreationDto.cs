using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models
{
    public class SetPieceForCreationDto
    {
        public int? SetId { get; set; }

        [Required]
        public int PieceId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
    }
}
