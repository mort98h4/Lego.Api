using Asp.Versioning;
using AutoMapper;
using Lego.Api.Entities;
using Lego.Api.Helpers;
using Lego.Api.Models.SetPiece;
using Lego.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lego.Api.Controllers
{
    /// <summary>
    /// API Controller of set pieces
    /// </summary>
    [Route("api/v{version:apiVersion}/sets/{setId}")]
    [ApiController]
    [ApiVersion(1)]
    [Produces("application/json")]
    public class SetPiecesController : ControllerBase
    {
        private readonly ILogger<SetPiecesController> _logger;
        private readonly ILegoRepository _legoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="legoRepository"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SetPiecesController(ILogger<SetPiecesController> logger, ILegoRepository legoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legoRepository = legoRepository ?? throw new ArgumentNullException(nameof(legoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all missing pieces of a set
        /// </summary>
        /// <param name="setId">The id of the set to fetch the missing pieces from</param>
        /// <param name="pageNumber">The page to return</param>
        /// <param name="pageSize">Number of sets to return</param>
        /// <returns>A list of missing pieces of a set</returns>
        [HttpGet("missingPieces")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SetPieceWithPieceDto>>> GetMissingPiecesForSet(int setId, int pageNumber = 1, int pageSize = Constants.DefaultPageSize)
        {
            if (!await _legoRepository.SetExistsAsync(setId))
            {
                var message = $"Set with id '{setId}' was not found when accessing missing pieces.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            if (pageSize > Constants.MaxPageSize)
            {
                pageSize = Constants.MaxPageSize;
            }

            var (setPieceEntities, paginationMetadata) = await _legoRepository.GetSetMissingPieces(setId, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<SetPieceWithPieceDto>>(setPieceEntities));
        }

        /// <summary>
        /// Get a missing piece of a set
        /// </summary>
        /// <param name="setId">The id of the set</param>
        /// <param name="pieceId">The id of the piece</param>
        /// <returns>A missing piece of a set</returns>
        [HttpGet("missingPieces/{pieceId}", Name = "GetMissingPiece")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMissingPieceForSet(int setId, int pieceId)
        {
            if (!await _legoRepository.SetExistsAsync(setId))
            {
                var message = $"Set with id '{setId}' was not found when accessing missing pieces.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            var setPieceEntity = await _legoRepository.GetSetMissingPiece(setId, pieceId);
            if (setPieceEntity == null)
            {
                var message = $"Piece with id '{pieceId}' was not found when accessing missing pieces of set with id '{setId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            return Ok(_mapper.Map<SetPieceWithPieceDto>(setPieceEntity));
        }

        /// <summary>
        /// Create a new missing piece of a set 
        /// </summary>
        /// <param name="setId">The id of the set</param>
        /// <param name="setPiece">The missing piece to add</param>
        /// <returns></returns>
        [HttpPost("missingPieces")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SetPieceWithPieceDto>> CreateMissingPieceForSet(int setId, SetPieceForCreationDto setPiece)
        {
            if (!await _legoRepository.SetExistsAsync(setId))
            {
                var message = $"Set with id '{setId}' was not found while trying to add a missing piece.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            var pieceEntity = await _legoRepository.GetPieceByIdAsync(setPiece.PieceId);
            if (pieceEntity == null)
            {
                var message = $"Piece with id '{setPiece.PieceId}' was not found while trying to add it to missing pieces of set with id '{setId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            if (await _legoRepository.SetMissingPieceExistsAsync(setId, setPiece.PieceId))
            {
                var message = $"Set with id '{setId}' already has a missing piece with id '{setPiece.PieceId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 400, "Bad Request");
            }

            setPiece.SetId = setId;
            var finalMissingPiece = _mapper.Map<SetPiece>(setPiece);

            _legoRepository.CreateSetMissingPiece(finalMissingPiece);
            await _legoRepository.SaveChangesAsync();

            var createdMissingPiece = _mapper.Map<SetPieceWithPieceDto>(finalMissingPiece);

            return CreatedAtRoute("GetMissingPiece", new
            {
                setId = setId,
                pieceId = createdMissingPiece.Piece.Id
            }, createdMissingPiece);
        }

        /// <summary>
        /// Update a missing piece of a set
        /// </summary>
        /// <param name="setId">The id of the set</param>
        /// <param name="pieceId">The id of the piece</param>
        /// <param name="setPiece">The missing piece for updating</param>
        /// <returns>The updated missing piece of a set</returns>
        [HttpPut("missingPieces/{pieceId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SetPieceWithPieceDto>> UpdateMissingPieceForSet(int setId, int pieceId,
            SetPieceForUpdatingDto setPiece)
        {
            if (!await _legoRepository.SetExistsAsync(setId))
            {
                var message = $"Set with id '{setId}' was not found while trying to update a missing piece with id '{pieceId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            if (!await _legoRepository.PieceExistsAsync(pieceId))
            {
                var message = $"Piece with id '{pieceId}' was not found while trying to update the missing piece of set with id '{setId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            var setPieceEntity = await _legoRepository.GetSetMissingPiece(setId, pieceId);
            if (setPieceEntity == null)
            {
                var message = $"Set with id '{setId}' has no missing piece with id '{pieceId}'.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            setPiece.Quantity = setPiece.Quantity > 0 ? setPiece.Quantity : setPieceEntity.Quantity;

            var finalSetPiece = _mapper.Map(setPiece, setPieceEntity);
            await _legoRepository.SaveChangesAsync();

            var updatedSetPiece = _mapper.Map<SetPieceWithPieceDto>(finalSetPiece);

            return Ok(updatedSetPiece);
        }
    }
}
