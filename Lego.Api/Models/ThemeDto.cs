namespace Lego.Api.Models
{
    /// <summary>
    /// A specific theme of Lego sets 
    /// </summary>
    public class ThemeDto
    {
        /// <summary>
        /// The id of the theme
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the theme
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the theme
        /// </summary>
        public string? Description { get; set; }
    }
}
