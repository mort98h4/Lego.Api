using Lego.Api.Entities;

namespace Lego.Api.Services
{
    public interface ILegoRepository
    {
        Task<IEnumerable<Set>> GetSetsAsync();
        Task<Set?> GetSetAsync(int setId);
        Task<IEnumerable<Theme>> GetThemesAsync();
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
