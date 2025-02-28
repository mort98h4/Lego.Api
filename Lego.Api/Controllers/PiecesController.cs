using Asp.Versioning;
using AutoMapper;
using Lego.Api.Entities;
using Lego.Api.Helpers;
using Lego.Api.Models.Piece;
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
    [Produces("application/json")]
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

        /// <summary>
        /// Get a Lego piece by id
        /// </summary>
        /// <param name="pieceId">The id of the piece to get</param>
        /// <returns>A Lego piece</returns>
        [HttpGet("{pieceId}", Name = "GetPiece")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPiece(int pieceId)
        {
            var piece = await _legoRepository.GetPieceByIdAsync(pieceId);
            if (piece == null)
            {
                var message = $"Piece with id '{pieceId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            return Ok(_mapper.Map<PieceDto>(piece));
        }

        /// <summary>
        /// Create a new Lego piece
        /// </summary>
        /// <param name="piece">The piece for creation</param>
        /// <returns>The newly created piece</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PieceDto>> CreatePiece(PieceForCreationDto piece)
        {
            var finalPiece = _mapper.Map<Piece>(piece);

            _legoRepository.CreatePiece(finalPiece);
            await _legoRepository.SaveChangesAsync();

            var createdPiece = _mapper.Map<PieceDto>(finalPiece);

            return CreatedAtRoute("GetPiece", new
            {
                pieceId = createdPiece.Id
            }, createdPiece);
        }

        /// <summary>
        /// Update a Lego piece
        /// </summary>
        /// <param name="pieceId">The id of the piece to update</param>
        /// <param name="piece">The piece for updating</param>
        /// <returns>The updated piece</returns>
        [HttpPut("{pieceId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PieceDto>> UpdatePiece(int pieceId, PieceForUpdatingDto piece)
        {
            var pieceEntity = await _legoRepository.GetPieceByIdAsync(pieceId);
            if (pieceEntity == null)
            {
                var message = $"Piece with id '{pieceId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            piece.PieceNo = string.IsNullOrWhiteSpace(piece.PieceNo) ? pieceEntity.PieceNo : piece.PieceNo;
            piece.Color = string.IsNullOrWhiteSpace(piece.Color) ? pieceEntity.Color : piece.Color;
            piece.Description = string.IsNullOrWhiteSpace(piece.Description) ? pieceEntity.Description : piece.Description;

            var finalPiece = _mapper.Map(piece, pieceEntity);
            await _legoRepository.SaveChangesAsync();

            var updatedPiece = _mapper.Map<PieceDto>(finalPiece);

            return Ok(updatedPiece);
        }

        /// <summary>
        /// Delete a Lego piece
        /// </summary>
        /// <param name="pieceId">The id of the piece for deletion</param>
        /// <returns>No content</returns>
        [HttpDelete("{pieceId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePiece(int pieceId)
        {
            var pieceEntity = await _legoRepository.GetPieceByIdAsync(pieceId);
            if (pieceEntity == null)
            {
                var message = $"Piece with id '{pieceId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            _legoRepository.DeletePiece(pieceEntity);
            await _legoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
