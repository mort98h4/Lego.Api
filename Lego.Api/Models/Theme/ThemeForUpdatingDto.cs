using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.Theme
{
    /// <summary>
    /// A Lego theme for updating
    /// </summary>
    public class ThemeForUpdatingDto
    {
        /// <summary>
        /// The name of the theme
        /// </summary>
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// The description of the theme
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
