using System.Text.Json;

namespace ProductionTracker.Api.Seed
{
    /// <summary>
    /// Loads catalog seed data from JSON files.
    /// </summary>
    /// <remarks>
    /// This class is responsible for deserializing seed files into
    /// <see cref="CatalogPositionSeedDto"/> instances.
    /// It does not perform validation or persistence.
    /// </remarks>
    public class DemoCatalogSeeder
    {
        // <summary>
        /// Loads catalog position seed data from the specified JSON file.
        /// </summary>
        /// <param name="filePath">
        /// Path to the JSON file containing catalog seed data.
        /// </param>
        /// <returns>
        /// A list of catalog position seed DTOs.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the specified file does not exist.
        /// </exception>
        /// <exception cref="JsonException">
        /// Thrown when the file content cannot be deserialized.
        /// </exception>
        public List<CatalogPositionSeedDto> Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    $"Catalog seed file not found: {filePath}",
                    filePath);
            }

            var json = File.ReadAllText(filePath);

            var items = JsonSerializer.Deserialize<List<CatalogPositionSeedDto>>(json);

            if (items is null)
            {
                throw new JsonException("Failed to deserialize catalog seed data.");
            }

            return items ?? [];
        }
    }
}


