using Lego.Api.Entities;

namespace Lego.Api.Services
{
    public interface ILegoRepository
    {
        Task<(IEnumerable<Set>, PaginationMetadata)> GetSetsAsync(int? themeId, int? seriesId, string? searchQuery, int pageNumber, int pageSize);
        Task<Set?> GetSetByIdAsync(int setId);
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

        Task<bool> SaveChangesAsync();
    }
}
