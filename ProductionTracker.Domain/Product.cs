namespace ProductionTracker.Domain;

public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public string? Article { get; }
    public string? Characteristics { get; }
    public decimal? BasePrice { get; }

    public Product(
        Guid id,
        string name,
        string? article,
        string? characteristics,
        decimal? basePrice)
    {

        if (id == Guid.Empty)
            throw new ArgumentException("Product id must be set.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        Id = id;
        Name = name;
        Article = article;
        Characteristics = characteristics;
        BasePrice = basePrice;
    }
}


