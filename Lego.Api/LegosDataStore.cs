using Lego.Api.Models;

namespace Lego.Api
{
    /// <summary>
    /// Temporary data store 
    /// </summary>
    public class LegosDataStore
    {
        /// <summary>
        /// List of Lego sets
        /// </summary>
        public List<SetDto> Sets { get; set; }

        /// <summary>
        /// List of Lego parts
        /// </summary>
        public List<PartDto> Parts { get; set; }

        /// <summary>
        /// List of themes
        /// </summary>
        public List<ThemeDto> Themes { get; set; }

        /// <summary>
        /// List of collections
        /// </summary>
        public List<CollectionDto> Collections { get; set; }

        /// <summary>
        /// Temporary data store constructor
        /// </summary>
        public LegosDataStore() 
        {
            Parts = new List<PartDto>()
            {
                new PartDto()
                {
                    Id = 1,
                    PartNo = "30055",
                    Color = "Brown",
                    Description = "Fence 1 x 4 x 2 Spindled with 2 Studs"
                },
                new PartDto()
                {
                    Id = 2,
                    PartNo = "30374px1",
                    Color = "Dark Nougat",
                    Description = "Bar 4L (Lightsaber Blade / Wand) with Black Extended Half Circle and Dots Pattern (Musical Instrument, Flute)"
                }
            };

            Themes = new List<ThemeDto>()
            {
                new ThemeDto()
                {
                    Id = 1,
                    Name = "Star Wars",
                    Description = "One of the world's most popular franchises."
                },
                new ThemeDto()
                {
                    Id = 2,
                    Name = "Harry Potter",
                    Description = "Go on magical adventures with Harry, Ron, and Hermione."
                },
                new ThemeDto()
                {
                    Id = 3,
                    Name = "Castle"
                }
            };

            Collections = new List<CollectionDto>() 
            {
                new CollectionDto()
                {
                    Id = 1,
                    Name = "The Mid-Scale Starship Collection",
                    Description = "Small starships made for displaying."
                },
                new CollectionDto()
                {
                    Id = 2,
                    Name = "The Diorama Collection",
                    Description = "Scenes from the Star Wars Universe made as dioramas for displaying."
                },
                new CollectionDto()
                {
                    Id = 3,
                    Name = "Ultimate Collector Series",
                    Description = "Large sets made with the utmost care and focus on details."
                }
            };

            Sets = new List<SetDto>()
            {
                new SetDto()
                {
                    Id = 1,
                    ModelNo = "75404",
                    Name = "Acclamator-Class Assault Ship",
                    Theme = Themes[0],
                    Collection = Collections[0],
                    NoOfParts = 450,
                    Description = "A display piece of a Republic Era assault ship.",
                    Sealed = false,
                    HasBox = true,
                },
                new SetDto()
                {
                    Id = 2,
                    ModelNo = "4707",
                    Name = "Hagrid's Hut",
                    Theme = Themes[1],
                    NoOfParts = 288,
                    Description = "The first edition of Hagrid's Hut.",
                    Sealed = false,
                    HasBox = false,
                    MissingParts = new List<PartDto>()
                    {
                        Parts[0]
                    }
                },
                new SetDto()
                {
                    Id = 3,
                    ModelNo = "75313",
                    Name = "AT-AT",
                    Theme = Themes[0],
                    Collection = Collections[2],
                    NoOfParts = 6785,
                    Description = "This is the AT-AT that alle Lego Star Wars collectors have been waiting for.",
                    Sealed = false,
                    HasBox = true,
                }
            };
        }
    }
}
