using ProductionTracker.Api.Seed;
using ProductionTracker.Domain;

namespace ProductionTracker.Api.Bootstrap
{
    /// <summary>
    /// Initializes the in-memory product catalog using demo seed data.
    /// </summary>
    /// <remarks>
    /// This bootstrapper is intended for development and demonstration purposes.
    /// It loads catalog positions from a JSON seed file during application startup.
    /// </remarks>
    public class DemoCatalogBootstrapper
    {
        /// <summary>
        /// Loads catalog positions from seed data and adds them to the catalog.
        /// </summary>
        /// <param name="catalog">
        /// The in-memory catalog instance to be initialized.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the provided catalog instance is null.
        /// </exception>
        public void Initialize(InMemoryCatalog catalog)
        {
            ArgumentNullException.ThrowIfNull(catalog);

            DemoCatalogSeeder seeder = new();
            var dtos = seeder.Load("Seed/catalog.demo.json");

            foreach (var d in dtos)
            {
                catalog.AddProduct(
                                    d.Name,
                                    d.Article,
                                    d.Characteristics,
                                    d.BasePrice
                                    );
            }
        }
    }
}
