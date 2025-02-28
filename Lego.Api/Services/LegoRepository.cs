﻿using Lego.Api.DbContexts;
using Lego.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace Lego.Api.Services
{
    public class LegoRepository : ILegoRepository
    {
        private readonly LegoContext _context;

        public LegoRepository(LegoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<Set>, PaginationMetadata)> GetSetsAsync(int? themeId, int? seriesId, string? searchQuery, int pageNumber, int pageSize)
        {
            var sets = _context.Sets as IQueryable<Set>;

            if (themeId != null && themeId > 0)
            {
                sets = sets.Where(s => s.ThemeId == themeId);
            }

            if (seriesId != null && seriesId > 0)
            {
                sets = sets.Where(s => s.SeriesId == seriesId);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                sets = sets.Where(s => 
                    s.Name.ToLower().Contains(searchQuery) ||
                    s.ModelNo.Contains(searchQuery) ||
                    (s.Description != null && s.Description.ToLower().Contains(searchQuery)) ||
                    (s.Theme != null && s.Theme.Name.ToLower().Contains(searchQuery)) ||
                    (s.Series != null && s.Series.Name.ToLower().Contains(searchQuery))
                    );
            }

            var totalItemCount = await sets.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var setsToReturn = await sets
                .Include(s => s.Theme)
                .Include(s => s.Series)
                .Include(s => s.MissingPieces)
                .OrderBy(s => s.ModelNo)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (setsToReturn, paginationMetadata);
        }

        public async Task<Set?> GetSetByIdAsync(int setId)
        {
            return await _context.Sets.Where(s => s.Id == setId)
                .Include(s => s.Theme)
                .Include(s => s.Series)
                .FirstOrDefaultAsync();
        }

        public async Task<(Set?, IEnumerable<SetPiece>)> GetSetWithMissingPiecesByIdAsync(int setId)
        {
            var set = await _context.Sets.Where(s => s.Id == setId)
                .Include(s => s.Theme)
                .Include(s => s.Series)
                .Include(s => s.MissingPieces)
                .FirstOrDefaultAsync();

            var missingPieces = new List<SetPiece>();

            if (set != null)
            {
                missingPieces = await _context.SetMissingPieces.Where(mp => mp.SetId == setId).Include(mp => mp.Piece)
                    .ToListAsync();
            }

            return (set, missingPieces);
        }

        public async Task<bool> SetExistsAsync(int setId)
        {
            return await _context.Sets.AnyAsync(s => s.Id == setId);
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

        public async Task<Theme?> GetThemeByIdAsync(int themeId)
        {
            return await _context.Themes.Where(t => t.Id == themeId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ThemeExistsAsync(int themeId)
        {
            return await _context.Themes.AnyAsync(t => t.Id == themeId);
        }

        public void CreateTheme(Theme theme)
        {
            _context.Themes.Add(theme);
        }

        public void DeleteTheme(Theme theme)
        {
            _context.Themes.Remove(theme);
        }

        public async Task<(IEnumerable<Series>, PaginationMetadata)> GetSeriesAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var series = _context.Series as IQueryable<Series>;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                series = series.Where(s =>
                    s.Name.ToLower().Contains(searchQuery) ||
                    (s.Description != null && s.Description.ToLower().Contains(searchQuery)));
            }

            var totalItemCount = await series.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var seriesToReturn = await series
                .OrderBy(t => t.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (seriesToReturn, paginationMetadata);
        }

        public async Task<Series?> GetSeriesByIdAsync(int seriesId)
        {
            return await _context.Series.Where(c => c.Id == seriesId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SeriesExistsAsync(int seriesId)
        {
            return await _context.Series.AnyAsync(c => c.Id == seriesId);
        }

        public void CreateSeries(Series series)
        {
            _context.Series.Add(series);
        }

        public void DeleteSeries(Series series)
        {
            _context.Series.Remove(series);
        }

        public async Task<(IEnumerable<Piece>, PaginationMetadata)> GetPiecesAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var pieces = _context.Pieces as IQueryable<Piece>;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                pieces = pieces.Where(p =>
                    p.PieceNo.ToLower().Contains(searchQuery) ||
                    p.Color.ToLower().Contains(searchQuery) ||
                    p.Description.ToLower().Contains(searchQuery));
            }

            var totalItemCount = await pieces.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var piecesToReturn = await pieces
                .OrderBy(p => p.PieceNo)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (piecesToReturn, paginationMetadata);
        }

        public async Task<Piece?> GetPieceByIdAsync(int pieceId)
        {
            return await _context.Pieces.Where(p => p.Id == pieceId).FirstOrDefaultAsync();
        }

        public async Task<bool> PieceExistsAsync(int pieceId)
        {
            return await _context.Pieces.AnyAsync(p => p.Id == pieceId);
        }

        public void CreatePiece(Piece piece)
        {
            _context.Pieces.Add(piece);
        }

        public void DeletePiece(Piece piece)
        {
            _context.Pieces.Remove(piece);
        }

        public async Task<(IEnumerable<SetPiece>, PaginationMetadata)> GetSetMissingPieces(int setId, int pageNumber, int pageSize)
        {
            var missingPieces = _context.SetMissingPieces as IQueryable<SetPiece>;

            missingPieces = missingPieces.Where(mp => mp.SetId == setId).Include(mp => mp.Piece);

            var totalItemCount = await missingPieces.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var missingPiecesToReturn = await missingPieces
                .OrderBy(mp => mp.Piece.PieceNo)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (missingPiecesToReturn, paginationMetadata);
        }

        public async Task<SetPiece?> GetSetMissingPiece(int setId, int pieceId)
        {
            return await _context.SetMissingPieces.Where(mp =>
                    mp.SetId == setId && mp.PieceId == pieceId)
                .Include(mp => mp.Piece)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SetMissingPieceExistsAsync(int setId, int pieceId)
        {
            var setPieces = _context.SetMissingPieces as IQueryable<SetPiece>;
            setPieces = setPieces.Where(sp => 
                sp.SetId == setId &&
                sp.PieceId == pieceId);
            return await setPieces.AnyAsync();
        }

        public void CreateSetMissingPiece(SetPiece setPiece)
        {
            _context.SetMissingPieces.Add(setPiece);
        }

        public void DeleteSetMissingPiece(SetPiece setPiece)
        {
            _context.SetMissingPieces.Remove(setPiece);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
