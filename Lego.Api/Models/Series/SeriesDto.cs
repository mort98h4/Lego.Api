namespace Lego.Api.Models.Series
{

    /// <summary>
    /// A series of Lego sets under a specific theme
    /// </summary>
    public class SeriesDto
    {
        /// <summary>
        /// The id of the series
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the series
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the series
        /// </summary>
        public string? Description { get; set; }
    }
}
