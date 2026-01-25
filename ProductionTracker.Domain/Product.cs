using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionTracker.Domain;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public Product(string name, int quantity)
    {
        Id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;

        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        Quantity = quantity;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount > 0) Quantity += amount;
        else
            throw new ArgumentOutOfRangeException(nameof(amount));

        
    }
    public void DecreaseQuantity(int amount)
    {
        if (amount > 0 && amount <= Quantity) Quantity -= amount;
        else
            throw new ArgumentOutOfRangeException(nameof(amount));
    }
}

