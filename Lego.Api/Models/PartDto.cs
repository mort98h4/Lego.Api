namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego part
    /// </summary>
    public class PartDto
    {
        /// <summary>
        /// The id of the part
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The part number of the part
        /// </summary>
        public string PartNo { get; set; } = string.Empty;

        /// <summary>
        /// The color of the part
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// The description of the part
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The amount of identical parts that are missing from a set
        /// </summary>
        public int Quantity { get; set; }
    }
}
