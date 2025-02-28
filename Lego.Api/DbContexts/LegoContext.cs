using Lego.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lego.Api.DbContexts
{
    public class LegoContext : DbContext
    {
        public DbSet<Set> Sets { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SetPiece> SetMissingPieces { get; set; }

        public LegoContext(DbContextOptions<LegoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theme>().HasData(
                new Theme("Star Wars")
                {
                    Id = 1,
                    Description = "One of the world's most popular franchises."
                },
                new Theme("Harry Potter")
                {
                    Id = 2,
                    Description = "Go on magical adventures with Harry, Ron, and Hermione."
                });

            modelBuilder.Entity<Series>().HasData(
                new Series("The Mid-Scale Starship Series")
                {
                    Id = 1,
                    Description = "Small starships made for displaying."
                },
                new Series("Ultimate Collector's Series")
                {
                    Id = 2,
                    Description = "Large sets made with the utmost care and focus on details."
                });

            modelBuilder.Entity<Set>().HasData(
                new Set(
                    "75404",
                    "Acclamator-Class Assault Ship",
                    1,
                    450,
                    1,
                    1)
                {
                    Id = 1,
                    SeriesId = 1,
                    Description = "A display piece of a Republic Era assault ship."
                },
                new Set(
                    "4707",
                    "Hagrid's Hut",
                    2,
                    288,
                    0,
                    0)
                {
                    Id = 2,
                    Description = "The first edition of Hagrid's Hut."
                },
                new Set(
                    "75313",
                    "AT-AT",
                    1,
                    6785,
                    0,
                    1)
                {
                    Id = 3,
                    SeriesId = 2,
                    Description = "This is the AT-AT that alle Lego Star Wars collectors have been waiting for."
                });

            modelBuilder.Entity<Piece>().HasData(
                new Piece(
                    "30055",
                    "Brown",
                    "Fence 1 x 4 x 2 Spindled with 2 Studs")
                {
                    Id = 1
                });

            modelBuilder.Entity<SetPiece>().HasKey(e => new { e.SetId, e.PieceId });
        }
    }
}
