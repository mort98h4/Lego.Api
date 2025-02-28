﻿using System.ComponentModel;
using Asp.Versioning;
using AutoMapper;
using Lego.Api.Entities;
using Lego.Api.Helpers;
using Lego.Api.Models.Set;
using Lego.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;

namespace Lego.Api.Controllers
{
    /// <summary>
    /// API Controller of Lego sets
    /// </summary>
    [Route("api/v{version:apiVersion}/sets")]
    [ApiController]
    [ApiVersion(1)]
    [Produces("application/json")]
    public class SetsController : ControllerBase
    {
        private readonly ILogger<SetsController> _logger;
        private readonly ILegoRepository _legoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="legoRepository"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SetsController(ILogger<SetsController> logger, ILegoRepository legoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legoRepository = legoRepository ?? throw new ArgumentNullException(nameof(legoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all Lego sets
        /// </summary>
        /// <param name="themeId">Filter Lego sets by a known theme</param>
        /// <param name="seriesId">Filter Lego sets by a known series</param>
        /// <param name="searchQuery">Search Lego sets by model number, name, description. theme name or series name</param>
        /// <param name="pageNumber">The page to return</param>
        /// <param name="pageSize">Number of sets to return</param>
        /// <returns>A list of sets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SetDto>>> GetSets(int? themeId, int? seriesId, string? searchQuery, int pageNumber = 1, int pageSize = Constants.DefaultPageSize)
        {
            if (pageSize > Constants.MaxPageSize)
            {
                pageSize = Constants.MaxPageSize;
            }

            var (setEntities, paginationMetadata) = await _legoRepository.GetSetsAsync(themeId, seriesId, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<SetDto>>(setEntities));
        }

        /// <summary>
        /// Get a Lego set by id
        /// </summary>
        /// <param name="setId">The id of the set to get</param>
        /// <returns>A set</returns>
        [HttpGet("{setId}", Name = "GetSet")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSet(int setId)
        {
            var (set, missingPieces) = await _legoRepository.GetSetWithMissingPiecesByIdAsync(setId);

            if (set == null)
            {
                var message = $"Set with id '{setId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not found"); ;
            }

            return Ok(_mapper.Map<SetWithMissingPiecesDto>(set));
        }

        /// <summary>
        /// Create a new Lego set
        /// </summary>
        /// <param name="set">The set for creation</param>
        /// <returns>The newly created set</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SetDto>> CreateSet(SetForCreationDto set)
        {
            var theme = await _legoRepository.GetThemeByIdAsync(set.ThemeId);
            if (theme == null)
            {
                var message = $"Theme with id '{set.ThemeId}' was not found, when trying to create a new set.";
                _logger.LogInformation(message);

                return Problem(message, null, 404, "Not found");
            }

            
            if (set.SeriesId != null)
            {
                var collection = await _legoRepository.GetSeriesByIdAsync((int)set.SeriesId);
                if (collection == null)
                {
                    var message = $"Series with id '{set.SeriesId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);

                    return Problem(message, null, 404, "Not found");
                }
            }

            var finalSet = _mapper.Map<Set>(set);

            _legoRepository.CreateSet(finalSet);
            await _legoRepository.SaveChangesAsync();

            var createdSet = _mapper.Map<SetDto>(finalSet);

            return CreatedAtRoute("GetSet", new
            {
                setId = createdSet.Id
            }, createdSet);
        }

        /// <summary>
        /// Update a Lego set
        /// </summary>
        /// <param name="setId">The id of the set to update</param>
        /// <param name="set">The set for updating</param>
        /// <returns>The updated set</returns>
        [HttpPut("{setId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SetDto>> UpdateSet(int setId, SetForUpdatingDto set)
        {
            var setEntity = await _legoRepository.GetSetByIdAsync(setId);
            if (setEntity == null)
            {
                var message = $"Set with id '{setId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not found");
            }

            if (set.ThemeId != null)
            {
                var theme = await _legoRepository.GetThemeByIdAsync((int)set.ThemeId);
                if (theme == null)
                {
                    var message = $"Theme with id '{set.ThemeId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);
                    return Problem(message, null, 404, "Not found");
                }
            }

            if (set.SeriesId != null && set.SeriesId != -1)
            {
                var series = await _legoRepository.GetSeriesByIdAsync((int)set.SeriesId);
                if (series == null)
                {
                    var message = $"Series with id '{set.SeriesId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);
                    return Problem(message, null, 404, "Not found");
                }
            }

            if (set.SeriesId == -1)
            {
                set.SeriesId = null;
            }
            else
            {
                set.SeriesId = set.SeriesId > 0 ? set.SeriesId : setEntity.SeriesId;
            }

            set.ModelNo = string.IsNullOrWhiteSpace(set.ModelNo) ? setEntity.ModelNo : set.ModelNo;
            set.Name = string.IsNullOrWhiteSpace(set.Name) ? setEntity.Name : set.Name;
            set.ThemeId = set.ThemeId > 0 ? set.ThemeId : setEntity.ThemeId;
            set.Description = set.Description == null ? setEntity.Description : set.Description.Trim();
            set.Description = set.Description == "" ? null : set.Description;
            set.NoOfPieces = set.NoOfPieces > 0 ? set.NoOfPieces : setEntity.NoOfPieces;
            set.IsSealed = set.IsSealed != null ? set.IsSealed : Convert.ToBoolean(setEntity.IsSealed);
            set.HasBox = set.HasBox != null ? set.HasBox : Convert.ToBoolean(setEntity.HasBox);

            var finalSet = _mapper.Map(set, setEntity);
            await _legoRepository.SaveChangesAsync();

            var updatedSet = _mapper.Map<SetDto>(finalSet);

            return Ok(updatedSet);
        }

        /// <summary>
        /// Delete a Lego set
        /// </summary>
        /// <param name="setId">The id of the set for deletion</param>
        /// <returns>No content</returns>
        [HttpDelete("{setId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteSet(int setId)
        {
            var setEntity = await _legoRepository.GetSetByIdAsync(setId);
            if (setEntity == null)
            {
                var message = $"Set with id '{setId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not found");
            }

            _legoRepository.DeleteSet(setEntity);
            await _legoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
