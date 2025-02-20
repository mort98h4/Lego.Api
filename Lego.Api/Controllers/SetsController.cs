using Asp.Versioning;
using Lego.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private readonly LegosDataStore _legosDataStore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="legosDataStore"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SetsController(ILogger<SetsController> logger, LegosDataStore legosDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legosDataStore = legosDataStore ?? throw new ArgumentNullException(nameof(legosDataStore));
        }

        /// <summary>
        /// Get all Lego sets
        /// </summary>
        /// <returns>A list of sets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SetDto>> GetSets()
        {
            var sets = _legosDataStore.Sets;
            return Ok(sets);
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
        public ActionResult<SetDto> GetSet(int setId)
        {
            var set = _legosDataStore.Sets.FirstOrDefault(s => s.Id == setId);
            if (set == null)
            {
                _logger.LogInformation($"Set with id '{setId}' was not found.");
                return NotFound();
            }

            return Ok(set);
        }

        /// <summary>
        /// Create a new Lego set
        /// </summary>
        /// <param name="set">The set for creation</param>
        /// <returns>The newly created set</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<SetDto> CreateSet(SetForCreationDto set)
        {
            var maxSetId = _legosDataStore.Sets.Max(s => s.Id);

            var finalSet = new SetDto()
            {
                Id = ++maxSetId,
                ModelNo = set.ModelNo,
                Name = set.Name,
                Theme = set.Theme,
                Collection = set.Collection,
                NoOfParts = set.NoOfParts,
                Description = set.Description,
                Sealed = set.Sealed,
                HasBox = set.HasBox,
                MissingParts = set.MissingParts
            };

            _legosDataStore.Sets.Add(finalSet);

            return CreatedAtRoute("GetSet", new
            {
                setId = finalSet.Id
            }, finalSet);
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
        public ActionResult<SetDto> UpdateSet(int setId, SetForUpdatingDto set)
        {
            var setFromStore = _legosDataStore.Sets.FirstOrDefault(s => s.Id == setId);
            if (setFromStore == null)
            {
                _logger.LogInformation($"Set with id '{setId}' was not found.");
                return NotFound();
            } 

            setFromStore.ModelNo = set.ModelNo ?? setFromStore.ModelNo;
            setFromStore.Name = set.Name ?? setFromStore.Name;
            setFromStore.Theme = set.Theme ?? setFromStore.Theme;
            setFromStore.Collection = set.Collection ?? setFromStore.Collection;
            setFromStore.Description = set.Description ?? setFromStore.Description;
            setFromStore.NoOfParts = set.NoOfParts ?? setFromStore.NoOfParts;
            setFromStore.Sealed = set.Sealed ?? setFromStore.Sealed;
            setFromStore.HasBox = set.HasBox ?? setFromStore.HasBox;
            setFromStore.MissingParts = set.MissingParts ?? setFromStore.MissingParts;

            return Ok(setFromStore);
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
        public ActionResult DeleteSet(int setId)
        {
            var setFromStore = _legosDataStore.Sets.FirstOrDefault(s => s.Id == setId);
            if (setFromStore == null)
            {
                _logger.LogInformation($"Set with id '{setId}' was not found.");
                return NotFound();
            }

            _legosDataStore.Sets.Remove(setFromStore);

            return NoContent();
        }
    }
}
