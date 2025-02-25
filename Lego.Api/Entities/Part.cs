using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Entities
{
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PartNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<Set> Sets { get; } = new List<Set>();

        //[Required]
        //[Range(1, Int32.MaxValue)]
        //public int Quantity { get; set; }

        //[ForeignKey("SetId")]
        //public Set? Set { get; set; }

        //[Required]
        //public int SetId { get; set; }

        public Part(string partNo, string color, string description)
        {
            PartNo = partNo;
            Color = color;
            Description = description;
            //Quantity = quantity;
            //SetId = setId;
        }
    }
}
