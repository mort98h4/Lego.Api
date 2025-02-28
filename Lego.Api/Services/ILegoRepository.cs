using Lego.Api.Entities;

namespace Lego.Api.Services
{
    public interface ILegoRepository
    {
        Task<(IEnumerable<Set>, PaginationMetadata)> GetSetsAsync(int? themeId, int? seriesId, string? searchQuery, int pageNumber, int pageSize);
        Task<Set?> GetSetByIdAsync(int setId);
        Task<(Set?, IEnumerable<SetPiece>)> GetSetWithMissingPiecesByIdAsync(int setId);
        Task<bool> SetExistsAsync(int setId);
        void CreateSet(Set set);
        void DeleteSet(Set set);

        Task<(IEnumerable<Theme>, PaginationMetadata)> GetThemesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<Theme?> GetThemeByIdAsync(int themeId);
        Task<bool> ThemeExistsAsync(int themeId);
        void CreateTheme(Theme theme);
        void DeleteTheme(Theme theme);
        
        Task<(IEnumerable<Series>, PaginationMetadata)> GetSeriesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<Series?> GetSeriesByIdAsync(int seriesId);
        Task<bool> SeriesExistsAsync(int seriesId);
        void CreateSeries(Series series);
        void DeleteSeries(Series series);

        Task<(IEnumerable<Piece>, PaginationMetadata)> GetPiecesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<Piece?> GetPieceByIdAsync(int pieceId);
        void CreatePiece(Piece piece);
        void DeletePiece(Piece piece);

        Task<(IEnumerable<SetPiece>, PaginationMetadata)> GetSetMissingPieces(int setId, int pageNumber, int pageSize);
        Task<SetPiece?> GetSetMissingPiece(int setId, int pieceId);
        Task<bool> SetMissingPieceExistsAsync(int setId, int pieceId);
        void CreateSetMissingPiece(SetPiece setPiece);


        Task<bool> SaveChangesAsync();
    }
}
