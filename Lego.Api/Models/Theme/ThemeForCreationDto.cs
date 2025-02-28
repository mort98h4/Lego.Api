using System.ComponentModel.DataAnnotations;

namespace Lego.Api.Models.Theme
{
    /// <summary>
    /// A Lego theme for creation
    /// </summary>
    public class ThemeForCreationDto
    {
        /// <summary>
        /// The name of the theme
        /// </summary>
        [Required(ErrorMessage = "You must provide a name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the theme
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
