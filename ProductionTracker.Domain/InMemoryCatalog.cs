namespace ProductionTracker.Domain
{
    public class Position(Guid id, string name, string? article,
            string? characteristics,
            decimal? basePrice)
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public string? Article { get; private set; } = article;
        public string? Characteristics { get; private set; } = characteristics;
        public decimal? BasePrice { get; private set; } = basePrice;
    }
    public class InMemoryCatalog
    {
        private readonly List<Position> Positions = [];


        public IReadOnlyCollection<Position> GetAll()
        {
            return Positions.AsReadOnly();
        }

        /// <summary>
        /// Adds a new product position to the catalog.
        /// </summary>
        /// <param name="name">Product name.</param>
        /// <param name="article">Optional product article.</param>
        /// <param name="characteristics">Optional product characteristics.</param>
        /// <param name="basePrice">Optional base price.</param>
        /// <returns>
        /// The identifier of the created catalog position.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the product name is null or empty.
        /// </exception>
        public Guid AddProduct(
            string? name,
            string? article,
            string? characteristics,
            decimal? basePrice)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            Guid id = Guid.NewGuid();


            Position position = new(
                id,
                name,
                article,
                characteristics,
                basePrice);

            Positions.Add(position);

            return id;
        }

        public bool Exists(Guid id)
        {
            return Positions.Any(pos => pos.Id == id);
        }


        public Position? GetById(Guid id)
        {
            return Positions.FirstOrDefault(pos => pos.Id == id);
        }

        public IEnumerable<Position> FindByName(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return [];

            return Positions.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
