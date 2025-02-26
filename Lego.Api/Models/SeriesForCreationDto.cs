using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego series for creation
    /// </summary>
    public class SeriesForCreationDto
    {
        /// <summary>
        /// The name of the series
        /// </summary>
        [Required(ErrorMessage = "You must provide a name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the series
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
