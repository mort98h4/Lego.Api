using Lego.Api.Entities;

namespace Lego.Api.Services
{
    public interface ILegoRepository
    {
        Task<(IEnumerable<Set>, PaginationMetadata)> GetSetsAsync(int? themeId, int? collectionId, string? searchQuery, int pageNumber, int pageSize);
        Task<Set?> GetSetAsync(int setId);
        Task<(IEnumerable<Theme>, PaginationMetadata)> GetThemesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<Theme?> GetThemeAsync(int themeId);
        Task<IEnumerable<Collection>> GetCollectionsAsync();
        Task<Collection?> GetCollectionAsync(int collectionId);

        Task<bool> ThemeExistsAsync(int themeId);
        Task<bool> CollectionExistsAsync(int collectionId);

        void CreateSet(Set set);

        void DeleteSet(Set set);

        Task<bool> SaveChangesAsync();
    }
}
