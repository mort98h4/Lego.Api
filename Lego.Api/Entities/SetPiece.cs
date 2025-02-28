using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lego.Api.Entities
{
    public class SetPiece
    {
        [Required]
        public int SetId { get; set; }

        [Required]
        public int PieceId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("SetId")]
        public Set? Set { get; set; }

        [ForeignKey("PieceId")]
        public Piece? Piece { get; set; }

        public SetPiece(int setId, int pieceId)
        {
            SetId = setId;
            PieceId = pieceId;
        }
    }
}
