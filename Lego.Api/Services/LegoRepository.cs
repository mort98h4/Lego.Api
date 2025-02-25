using Lego.Api.DbContexts;
using Lego.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lego.Api.Services
{
    public class LegoRepository : ILegoRepository
    {
        private readonly LegoContext _context;

        public LegoRepository(LegoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Set>> GetSetsAsync()
        {
            return await _context.Sets
                .Include(s => s.Theme)
                .Include(s => s.Collection)
                .Include(s => s.MissingParts)
                .OrderBy(s => s.ModelNo)
                .ToListAsync();
        }

        public async Task<Set?> GetSetAsync(int setId)
        {
            return await _context.Sets.Where(s => s.Id == setId)
                .Include(s => s.Theme)
                .Include(s => s.Collection)
                .Include(s => s.MissingParts)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Theme>> GetThemesAsync()
        {
            return await _context.Themes
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Theme?> GetThemeAsync(int themeId)
        {
            return await _context.Themes.Where(t => t.Id == themeId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Collection>> GetCollectionsAsync()
        {
            return await _context.Collections
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Collection?> GetCollectionAsync(int collectionId)
        {
            return await _context.Collections.Where(c => c.Id == collectionId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ThemeExistsAsync(int themeId)
        {
            return await _context.Themes.AnyAsync(t => t.Id == themeId);
        }

        public async Task<bool> CollectionExistsAsync(int collectionId)
        {
            return await _context.Collections.AnyAsync(c => c.Id == collectionId);
        }

        public void CreateSet(Set set)
        {
            _context.Sets.Add(set);
        }

        public void DeleteSet(Set set)
        {
            _context.Sets.Remove(set);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
