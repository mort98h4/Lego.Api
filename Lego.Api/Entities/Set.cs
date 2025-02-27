using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lego.Api.Entities
{
    public class Set
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string ModelNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("ThemeId")]
        public Theme? Theme { get; set; }

        [Required]
        public int ThemeId { get; set; }

        [ForeignKey("SeriesId")]
        public Series? Series { get; set; }

        public int? SeriesId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int NoOfPieces { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [Range(0,1)]
        public int IsSealed { get; set; }

        [Required]
        [Range(0, 1)]
        public int HasBox { get; set; }

        public ICollection<Piece> MissingPieces { get; } = new List<Piece>();

        public Set(string modelNo, string name, int themeId, int noOfPieces, int isSealed, int hasBox)
        {
            ModelNo = modelNo;
            Name = name;
            ThemeId = themeId;
            NoOfPieces = noOfPieces;
            IsSealed = isSealed;
            HasBox = hasBox;
        }
    }
}
