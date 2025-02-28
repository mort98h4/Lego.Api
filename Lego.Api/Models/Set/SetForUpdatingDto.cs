using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.Set
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
        /// The theme id of the set
        /// </summary>
        public int? ThemeId { get; set; }

        /// <summary>
        /// The collection id of the set
        /// </summary>
        public int? SeriesId { get; set; }

        /// <summary>
        /// The number of pieces included in the set
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Number of pieces must be at least 1")]
        public int? NoOfPieces { get; set; }

        /// <summary>
        /// The description of the set
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// If the set is sealed
        /// </summary>
        public bool? IsSealed { get; set; }

        /// <summary>
        /// If the set still has it's box
        /// </summary>
        public bool? HasBox { get; set; }

        ///// <summary>
        ///// A list of the missing pieces
        ///// </summary>
        //public ICollection<PartDto>? MissingPieces { get; set; }
    }
}
