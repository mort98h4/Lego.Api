namespace Lego.Api.Models
{
    public class SetDto
    {
        public int Id { get; set; }
        public string ModelNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ThemeDto Theme { get; set; } = new ThemeDto();
        public CollectionDto? Collection { get; set; }
        public int NoOfParts {get; set; }
        public string? Description { get; set; }
        public bool Sealed { get; set; } = false;
        public bool HasBox { get; set; } = true;
        public bool HasMissingParts { get; set; } = false;

        public int? NoOfMissingParts
        {
            get
            {
                return MissingParts?.Count;
            }
        }

        public ICollection<PartDto>? MissingParts { get; set; } = new List<PartDto>();
    }
}
