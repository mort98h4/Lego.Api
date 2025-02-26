using Asp.Versioning;
using AutoMapper;
using Lego.Api.Entities;
using Lego.Api.Helpers;
using Lego.Api.Models;
using Lego.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lego.Api.Controllers
{
    /// <summary>
    /// API Controller of Lego series
    /// </summary>
    [Route("api/v{version:apiVersion}/series")]
    [ApiController]
    [ApiVersion(1)]
    public class SeriesController : ControllerBase
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly ILegoRepository _legoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="legoRepository"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SeriesController(ILogger<SeriesController> logger, ILegoRepository legoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legoRepository = legoRepository ?? throw new ArgumentNullException(nameof(legoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all Lego series
        /// </summary>
        /// <param name="searchQuery">Search Lego series by name or description</param>
        /// <param name="pageNumber">The page to return</param>
        /// <param name="pageSize">The number of series to return</param>
        /// <returns>A list of Lego series</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SeriesDto>> GetSeries(string? searchQuery, int pageNumber = 1,
            int pageSize = Constants.DefaultPageSize)
        {
            if (pageSize > Constants.MaxPageSize)
            {
                pageSize = Constants.MaxPageSize;
            }

            var (seriesEntities, paginationMetadata) =
                await _legoRepository.GetSeriesAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<SeriesDto>>(seriesEntities));
        }

        /// <summary>
        /// Get a Lego series by id
        /// </summary>
        /// <param name="seriesId">The id of the series to get</param>
        /// <returns>A Lego series</returns>
        [HttpGet("{seriesId}", Name = "GetSeries")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSeriesById(int seriesId)
        {
            var series = await _legoRepository.GetSeriesByIdAsync(seriesId);
            if (series == null)
            {
                var message = $"Series with id '{seriesId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            return Ok(_mapper.Map<SeriesDto>(series));
        }

        /// <summary>
        /// Create a new Lego series
        /// </summary>
        /// <param name="series">The series for creation</param>
        /// <returns>The newly created series</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ThemeDto>> CreateSeries(SeriesForCreationDto series)
        {
            var finalSeries = _mapper.Map<Series>(series);

            _legoRepository.CreateSeries(finalSeries);
            await _legoRepository.SaveChangesAsync();

            var createdSeries = _mapper.Map<SeriesDto>(finalSeries);

            return CreatedAtRoute("GetSeries", new
            {
                seriesId = createdSeries.Id
            }, createdSeries);
        }
    }
}
