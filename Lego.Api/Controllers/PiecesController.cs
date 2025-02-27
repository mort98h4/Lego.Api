using Asp.Versioning;
using AutoMapper;
using Lego.Api.Helpers;
using Lego.Api.Models;
using Lego.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lego.Api.Controllers
{
    /// <summary>
    /// API Controller of Lego pieces
    /// </summary>
    [Route("api/v{version:apiVersion}/pieces")]
    [ApiController]
    [ApiVersion(1)]
    public class PiecesController : ControllerBase
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
        public PiecesController(ILogger<SeriesController> logger, ILegoRepository legoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legoRepository = legoRepository ?? throw new ArgumentNullException(nameof(legoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all Lego pieces
        /// </summary>
        /// <param name="searchQuery">Search Lego pieces by piece number, color or description</param>
        /// <param name="pageNumber">The page to return</param>
        /// <param name="pageSize">Number of pieces to return</param>
        /// <returns>A list of Lego pieces</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PieceDto>> GetPieces(string? searchQuery, int pageNumber = 1,
            int pageSize = Constants.MaxPageSize)
        {
            if (pageSize > Constants.MaxPageSize)
            {
                pageSize = Constants.MaxPageSize;
            }

            var (pieceEntities, paginationMetadata) =
                await _legoRepository.GetPiecesAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<PieceDto>>(pieceEntities));
        }
    }
}
