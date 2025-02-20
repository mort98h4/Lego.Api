using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego set for updating
    /// </summary>
    public class SetForUpdatingDto
    {
        /// <summary>
        /// The model number of the set
        /// </summary>
        [MaxLength(10)]
        [RegularExpression("^[1-9][0-9-]*$", ErrorMessage = "Model number may only contain digits and dashes, and the first digit should be more than zero.")]
        public string? ModelNo { get; set; }

        /// <summary>
        /// The name of the set
        /// </summary>
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// The theme of the set
        /// </summary>
        public ThemeDto? Theme { get; set; }

        /// <summary>
        /// The collection of the set
        /// </summary>
        public CollectionDto? Collection { get; set; }

        /// <summary>
        /// The number of parts included in the set
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Number of parts must be at least 1")]
        public int? NoOfParts { get; set; }

        /// <summary>
        /// The description of the set
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// If the set is sealed
        /// </summary>
        public bool? IsSealed { get; set; } = false;

        /// <summary>
        /// If the set still has it's box
        /// </summary>
        public bool? HasBox { get; set; } = true;

        /// <summary>
        /// A list of the missing parts
        /// </summary>
        public ICollection<PartDto>? MissingParts { get; set; }
    }
}
