using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lego.Api.Entities
{
    public class SetPiece
    {
        [ForeignKey("SetId")]
        [Required]
        public int SetId { get; set; }

        [ForeignKey("PieceId")]
        [Required]
        public int PieceId { get; set; }

        public int Quantity { get; set; }
    }
}
