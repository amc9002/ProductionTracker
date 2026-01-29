namespace ProductionTracker.Application;

using ProductionTracker.Domain;

/// <summary>
/// Provides application logic for interacting with the product catalog.
/// </summary>
public class CatalogApplicationService
{
    private readonly InMemoryCatalog _catalog;

    public CatalogApplicationService(InMemoryCatalog catalog)
    {
        _catalog = catalog;
    }

    /// <summary>
    /// Gets all positions available in the catalog.
    /// </summary>
    public IEnumerable<Position> GetAllPositions()
    {
        return _catalog.GetAll();
    }

    /// <summary>
    /// Retrieves a specific catalog position by its unique identifier.
    /// </summary>
    public Position? GetPositionById(Guid id)
    {
        return _catalog.GetById(id);
    }

    /// <summary>
    /// Searches for positions by a partial name match.
    /// </summary>
    public IEnumerable<Position> SearchPositions(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return _catalog.GetAll();

        return _catalog.GetAll()
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Registers a new position in the catalog.
    /// </summary>
    /// <returns>The unique identifier of the newly created position.</returns>
    public Guid RegisterNewPosition(string name, string? article, string? characteristics, decimal? basePrice)
    {
        // We delegate the actual creation to the Domain (InMemoryCatalog)
        return _catalog.AddProduct(name, article, characteristics, basePrice);
    }
}
