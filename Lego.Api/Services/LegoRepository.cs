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

        public async Task<(IEnumerable<Set>, PaginationMetadata)> GetSetsAsync(int? themeId, int? collectionId, string? searchQuery, int pageNumber, int pageSize)
        {
            var sets = _context.Sets as IQueryable<Set>;

            if (themeId != null && themeId > 0)
            {
                sets = sets.Where(s => s.ThemeId == themeId);
            }

            if (collectionId != null && collectionId > 0)
            {
                sets = sets.Where(s => s.CollectionId == collectionId);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                sets = sets.Where(s => 
                    s.Name.ToLower().Contains(searchQuery) ||
                    s.ModelNo.Contains(searchQuery) ||
                    (s.Description != null && s.Description.ToLower().Contains(searchQuery)) ||
                    (s.Theme != null && s.Theme.Name.ToLower().Contains(searchQuery)) ||
                    (s.Collection != null && s.Collection.Name.ToLower().Contains(searchQuery))
                    );
            }

            var totalItemCount = await sets.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var setsToReturn = await sets
                .Include(s => s.Theme)
                .Include(s => s.Collection)
                .Include(s => s.MissingParts)
                .OrderBy(s => s.ModelNo)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (setsToReturn, paginationMetadata);
        }

        public async Task<Set?> GetSetAsync(int setId)
        {
            return await _context.Sets.Where(s => s.Id == setId)
                .Include(s => s.Theme)
                .Include(s => s.Collection)
                .Include(s => s.MissingParts)
                .FirstOrDefaultAsync();
        }

        public void CreateSet(Set set)
        {
            _context.Sets.Add(set);
        }

        public void DeleteSet(Set set)
        {
            _context.Sets.Remove(set);
        }

        public async Task<(IEnumerable<Theme>, PaginationMetadata)> GetThemesAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var themes = _context.Themes as IQueryable<Theme>;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                themes = themes.Where(t => 
                    t.Name.ToLower().Contains(searchQuery) ||
                    (t.Description != null && t.Description.ToLower().Contains(searchQuery)));
            }

            var totalItemCount = await themes.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var themesToReturn = await themes
                .OrderBy(t => t.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (themesToReturn, paginationMetadata);
        }

        public async Task<Theme?> GetThemeAsync(int themeId)
        {
            return await _context.Themes.Where(t => t.Id == themeId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ThemeExistsAsync(int themeId)
        {
            return await _context.Themes.AnyAsync(t => t.Id == themeId);
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

        public async Task<bool> CollectionExistsAsync(int collectionId)
        {
            return await _context.Collections.AnyAsync(c => c.Id == collectionId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
