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

        [ForeignKey("CollectionId")]
        public Collection? Collection { get; set; }

        public int? CollectionId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int NoOfParts { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [Range(0,1)]
        public int IsSealed { get; set; }

        [Required]
        [Range(0, 1)]
        public int HasBox { get; set; }

        public ICollection<Part> MissingParts { get; } = new List<Part>();

        public Set(string modelNo, string name, int themeId, int noOfParts, int isSealed, int hasBox)
        {
            ModelNo = modelNo;
            Name = name;
            ThemeId = themeId;
            NoOfParts = noOfParts;
            IsSealed = isSealed;
            HasBox = hasBox;
        }
    }
}
