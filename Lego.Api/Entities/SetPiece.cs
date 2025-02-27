using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lego.Api.Entities
{
    public class SetPiece
    {
        [ForeignKey("SetId")]
        public Set? Set { get; set; }
        
        [Required]
        public int SetId { get; set; }


        [ForeignKey("PieceId")]
        public Piece? Piece { get; set; }

        [Required]
        public int PieceId { get; set; }

        public int Quantity { get; set; }
    }
}
