using Microsoft.AspNetCore.Mvc;

using ProductionTracker.Application;
using ProductionTracker.Domain;

namespace ProductionTracker.Api.Controllers;

/// <summary>
/// API endpoints for managing and browsing the product catalog positions.
/// </summary>
[ApiController]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly CatalogApplicationService _catalogService;

    public CatalogController(CatalogApplicationService catalogService)
    {
        _catalogService = catalogService;
    }

    /// <summary>
    /// Retrieves all catalog positions.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Position>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_catalogService.GetAllPositions());
    }

    /// <summary>
    /// Searches for catalog positions by name (case-insensitive partial match).
    /// </summary>
    /// <param name="term">The string or letters to search for in the position name.</param>
    /// <returns>A list of positions matching the search term.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Position>), StatusCodes.Status200OK)]
    public IActionResult Search([FromQuery] string term)
    {
        // This method handles the "by letters in name" logic
        var results = _catalogService.SearchPositions(term);
        return Ok(results);
    }

    /// <summary>
    /// Adds a new position definition to the catalog.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public IActionResult AddPosition([FromBody] CreatePositionRequest request)
    {
        var id = _catalogService.RegisterNewPosition(
            request.Name,
            request.Article,
            request.Characteristics,
            request.BasePrice);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Gets a specific position by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var position = _catalogService.GetPositionById(id);
        return position != null ? Ok(position) : NotFound();
    }
}

/// <summary>
/// Data transfer object for creating a new catalog position.
/// </summary>
public record CreatePositionRequest(
    string Name,
    string? Article,
    string? Characteristics,
    decimal? BasePrice);
