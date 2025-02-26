using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego series for updating
    /// </summary>
    public class SeriesForUpdatingDto
    {
        /// <summary>
        /// The name of the series
        /// </summary>
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// The description of the series
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
