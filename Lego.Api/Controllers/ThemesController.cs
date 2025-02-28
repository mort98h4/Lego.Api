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
    /// API Controller of Lego themes
    /// </summary>
    [Route("api/v{version:apiVersion}/themes")]
    [ApiController]
    [ApiVersion(1)]
    [Produces("application/json")]
    public class ThemesController : ControllerBase
    {
        private readonly ILogger<ThemesController> _logger;
        private readonly ILegoRepository _legoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="legoRepository"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ThemesController(ILogger<ThemesController> logger, ILegoRepository legoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _legoRepository = legoRepository ?? throw new ArgumentNullException(nameof(legoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all Lego themes
        /// </summary>
        /// <param name="searchQuery">Search Lego themes by name or description</param>
        /// <param name="pageNumber">The page to return</param>
        /// <param name="pageSize">Number of themes to return</param>
        /// <returns>A list of Lego themes</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ThemeDto>> GetThemes(string? searchQuery, int pageNumber = 1,
            int pageSize = Constants.DefaultPageSize)
        {
            if (pageSize > Constants.MaxPageSize)
            {
                pageSize = Constants.MaxPageSize;
            }

            var (themesEntities, paginationMetadata) =
                await _legoRepository.GetThemesAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<ThemeDto>>(themesEntities));
        }

        /// <summary>
        /// Get a Lego theme by id
        /// </summary>
        /// <param name="themeId">The id of the theme to get</param>
        /// <returns>A Lego theme</returns>
        [HttpGet("{themeId}", Name = "GetTheme")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTheme(int themeId)
        {
            var theme = await _legoRepository.GetThemeByIdAsync(themeId);
            if (theme == null)
            {
                var message = $"Theme with id '{themeId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            return Ok(_mapper.Map<ThemeDto>(theme));
        }

        /// <summary>
        /// Create a new Lego theme
        /// </summary>
        /// <param name="theme">The theme for creation</param>
        /// <returns>The newly created theme</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ThemeDto>> CreateTheme(ThemeForCreationDto theme)
        {
            var finalTheme = _mapper.Map<Theme>(theme);

            _legoRepository.CreateTheme(finalTheme);
            await _legoRepository.SaveChangesAsync();

            var createdTheme = _mapper.Map<ThemeDto>(finalTheme);

            return CreatedAtRoute("GetTheme", new
            {
                themeId = createdTheme.Id
            }, createdTheme);
        }

        /// <summary>
        /// Update a Lego theme
        /// </summary>
        /// <param name="themeId">The id of the theme to update</param>
        /// <param name="theme">The theme for updating</param>
        /// <returns>The updated theme</returns>
        [HttpPut("{themeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ThemeDto>> UpdateTheme(int themeId, ThemeForUpdatingDto theme)
        {
            var themeEntity = await _legoRepository.GetThemeByIdAsync(themeId);
            if (themeEntity == null)
            {
                var message = $"Theme with id '{themeId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            theme.Name = string.IsNullOrWhiteSpace(theme.Name) ? themeEntity.Name : theme.Name;
            theme.Description = theme.Description == null ? themeEntity.Description : theme.Description.Trim();
            theme.Description = theme.Description == "" ? null : theme.Description;

            var finalTheme = _mapper.Map(theme, themeEntity);
            await _legoRepository.SaveChangesAsync();

            var updatedTheme = _mapper.Map<ThemeDto>(finalTheme);

            return Ok(updatedTheme);
        }

        /// <summary>
        /// Delete a Lego theme
        /// </summary>
        /// <param name="themeId">The id of the theme for deletion</param>
        /// <returns>No content</returns>
        [HttpDelete("{themeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTheme(int themeId)
        {
            var themeEntity = await _legoRepository.GetThemeByIdAsync(themeId);
            if (themeEntity == null)
            {
                var message = $"Theme with id '{themeId}' was not found.";
                _logger.LogInformation(message);
                return Problem(message, null, 404, "Not Found");
            }

            _legoRepository.DeleteTheme(themeEntity);
            await _legoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
