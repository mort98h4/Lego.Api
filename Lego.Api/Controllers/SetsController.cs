using Asp.Versioning;
using AutoMapper;
using Lego.Api.Entities;
using Lego.Api.Models;
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
        /// <returns>A list of sets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SetDto>>> GetSets()
        {
            var setEntities = await _legoRepository.GetSetsAsync();

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
            var set = await _legoRepository.GetSetAsync(setId);

            if (set == null)
            {
                var message = $"Set with id '{setId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not found"); ;
            }

            return Ok(_mapper.Map<SetDto>(set));
        }

        /// <summary>
        /// Create a new Lego set
        /// </summary>
        /// <param name="set">The set for creation</param>
        /// <returns>The newly created set</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SetDto>> CreateSet(SetForCreationDto set)
        {
            var theme = await _legoRepository.GetThemeAsync(set.ThemeId);
            if (theme == null)
            {
                var message = $"Theme with id '{set.ThemeId}' was not found, when trying to create a new set.";
                _logger.LogInformation(message);

                return Problem(message, null, 404, "Not found");
            }

            
            if (set.CollectionId != null)
            {
                var collection = await _legoRepository.GetCollectionAsync((int)set.CollectionId);
                if (collection == null)
                {
                    var message = $"Theme with id '{set.CollectionId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);

                    return Problem(message, null, 404, "Not found");
                }
            }

            var finalSet = _mapper.Map<Entities.Set>(set);

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
            var setEntity = await _legoRepository.GetSetAsync(setId);
            if (setEntity == null)
            {
                var message = $"Set with id '{setId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not found");
            }

            if (set.ThemeId != null)
            {
                var theme = await _legoRepository.GetThemeAsync((int)set.ThemeId);
                if (theme == null)
                {
                    var message = $"Theme with id '{set.ThemeId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);
                    return Problem(message, null, 404, "Not found");
                }
            }

            if (set.CollectionId != null && set.CollectionId != -1)
            {
                var collection = await _legoRepository.GetCollectionAsync((int)set.CollectionId);
                if (collection == null)
                {
                    var message = $"Collection with id '{set.CollectionId}' was not found, when trying to create a new set.";
                    _logger.LogInformation(message);

                    return Problem(message, null, 404, "Not found");
                }
            }

            if (set.CollectionId == -1)
            {
                set.CollectionId = null;
            }
            else
            {
                set.CollectionId = set.CollectionId > 0 ? set.CollectionId : setEntity.CollectionId;
            }

            set.ModelNo = string.IsNullOrWhiteSpace(set.ModelNo) ? setEntity.ModelNo : set.ModelNo;
            set.Name = string.IsNullOrWhiteSpace(set.Name) ? setEntity.Name : set.Name;
            set.ThemeId = set.ThemeId > 0 ? set.ThemeId : setEntity.ThemeId;
            set.Description = string.IsNullOrWhiteSpace(set.Description) ? setEntity.Description : set.Description;
            set.NoOfParts = set.NoOfParts > 0 ? set.NoOfParts : setEntity.NoOfParts;
            set.IsSealed = set.IsSealed != null ? set.IsSealed : Convert.ToBoolean(setEntity.IsSealed);
            set.HasBox = set.HasBox != null ? set.HasBox : Convert.ToBoolean(setEntity.HasBox);

            var updatedSet = _mapper.Map(set, setEntity);
            await _legoRepository.SaveChangesAsync();

            var setToReturn = _mapper.Map<SetDto>(updatedSet);

            return Ok(setToReturn);
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
            var setEntity = await _legoRepository.GetSetAsync(setId);
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
