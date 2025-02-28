using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.Set
{
    /// <summary>
    /// A Lego set for creation
    /// </summary>
    public class SetForCreationDto
    {
        /// <summary>
        /// The model number of the set
        /// </summary>
        [Required(ErrorMessage = "You must provide a model number")]
        [MaxLength(10)]
        [RegularExpression("^[1-9][0-9-]*$", ErrorMessage = "Model number may only contain digits and dashes, and the first digit should be more than zero.")]
        public string ModelNo { get; set; } = string.Empty;

        /// <summary>
        /// The name of the set
        /// </summary>
        [Required(ErrorMessage = "You must provide a name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The theme id of the set
        /// </summary>
        [Required(ErrorMessage = "You must provide a theme id")]
        public int ThemeId { get; set; }

        /// <summary>
        /// The collection id of the set
        /// </summary>
        public int? SeriesId { get; set; }

        /// <summary>
        /// The number of pieces included in the set
        /// </summary>
        [Required(ErrorMessage = "You must provide the number of pieces")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of pieces must be at least 1")]
        public int NoOfPieces { get; set; }

        /// <summary>
        /// The description of the set
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// If the set is sealed
        /// </summary>
        public bool IsSealed { get; set; }

        /// <summary>
        /// If the set still has it's box
        /// </summary>
        public bool HasBox { get; set; }
    }
}
