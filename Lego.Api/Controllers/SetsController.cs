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
    }
}
