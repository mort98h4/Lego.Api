namespace Lego.Api.Models
{
    /// <summary>
    /// A Lego set
    /// </summary>
    public class SetDto
    {
        /// <summary>
        /// The id of the set
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The model number of the set
        /// </summary>
        public string ModelNo { get; set; } = string.Empty;

        /// <summary>
        /// The name of the set
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The theme of the set
        /// </summary>
        public ThemeDto Theme { get; set; } = new ThemeDto();

        /// <summary>
        /// The collection of the set
        /// </summary>
        public SeriesDto? Series { get; set; }

        /// <summary>
        /// The number of pieces included in the set
        /// </summary>
        public int NoOfPieces {get; set; }

        /// <summary>
        /// The description of the set
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// If the set is sealed
        /// </summary>
        public bool IsSealed { get; set; }

        /// <summary>
        /// If the set still has it's box
        /// </summary>
        public bool HasBox { get; set; }

        /// <summary>
        /// If the set has missing pieces
        /// </summary>
        public bool HasMissingPieces
        {
            get
            {
                return MissingPieces?.Count != 0;
            }
        }

        /// <summary>
        /// A list of the missing pieces
        /// </summary>
        public ICollection<SetPieceDto>? MissingPieces { get; set; } = new List<SetPieceDto>();
    }
}
