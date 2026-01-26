using System.Text.Json.Serialization;

namespace ProductionTracker.Api.Seed
{
    /// <summary>
    /// Data transfer object used to load catalog positions from seed JSON files.
    /// </summary>
    /// <remarks>
    /// This DTO represents the raw structure of catalog data as defined in seed files.
    /// It is intended for application startup.
    /// </remarks>
    public class CatalogPositionSeedDto
    {
        /// <summary>
        /// Name of the product position.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Optional product article or SKU identifier.
        /// </summary>
        [JsonPropertyName("article")]
        public string? Article { get; set; }

        /// <summary>
        /// Optional textual description or technical characteristics of the product.
        /// </summary>
        [JsonPropertyName("characteristics")]
        public string? Characteristics { get; set; }

        /// <summary>
        /// Optional base price of the product.
        /// </summary>
        [JsonPropertyName("basePrice")]
        public decimal? BasePrice { get; set; }
    }
}
