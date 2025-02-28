using Lego.Api.Models.Series;
using Lego.Api.Models.SetPiece;
using Lego.Api.Models.Theme;

namespace Lego.Api.Models.Set
{
    /// <summary>
    /// A Lego set with a list of missing pieces
    /// </summary>
    public class SetWithMissingPiecesDto
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
        public int NoOfPieces { get; set; }

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
        /// The number of missing pieces
        /// </summary>
        public int? NoOfMissingPieces
        {
            get
            {
                return MissingPieces?.Count;
            }
        }

        /// <summary>
        /// A list of the missing pieces
        /// </summary>
        public ICollection<SetPieceWithPieceDto>? MissingPieces { get; set; } = new List<SetPieceWithPieceDto>();
    }
}
