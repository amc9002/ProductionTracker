using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductionTracker.Domain;

namespace ProductionTracker.Application;

public class OrderService
{
    public static Order CreateOrder(Guid productId, int quantity)
    {
        var order = new Order(productId, quantity);
        return order;
    }
}

