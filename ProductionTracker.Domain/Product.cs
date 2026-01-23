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

    public Product(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}

