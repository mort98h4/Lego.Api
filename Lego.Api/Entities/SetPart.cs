using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lego.Api.Entities
{
    public class SetPart
    {
        [ForeignKey("SetId")]
        [Required]
        public int SetId { get; set; }

        [ForeignKey("PartId")]
        [Required]
        public int PartId { get; set; }

        public int Quantity { get; set; }
    }
}
