using Microsoft.AspNetCore.Mvc;

using ProductionTracker.Domain;

namespace ProductionTracker.Api.Controllers
{
    /// <summary>
    /// Provides read-only access to catalog positions.
    /// </summary>
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly InMemoryCatalog _catalog;
        public CatalogController(InMemoryCatalog catalog)
        {
            _catalog = catalog;
        }

        /// <summary>
        /// Returns all available catalog positions.
        /// </summary>
        /// <returns>
        /// A collection of catalog positions.
        /// </returns>
        [HttpGet]
        public IActionResult Show()
        {
            return Ok(_catalog.GetAll());
        }

    }
}
