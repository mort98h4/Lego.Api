namespace Lego.Api.Models
{
    public class PartDto
    {
        public int Id { get; set; }
        public string PartNo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
