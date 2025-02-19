namespace Lego.Api.Models
{

    /// <summary>
    /// A collection of Lego sets under a specific theme
    /// </summary>
    public class CollectionDto
    {
        /// <summary>
        /// The id of the collection
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the collection
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the collection
        /// </summary>
        public string? Description { get; set; }
    }
}
