using Microsoft.AspNetCore.Mvc;

using ProductionTracker.Application;
using ProductionTracker.Infrastructure;
namespace ProductionTracker.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly AppDbContext _db;

    public OrdersController(OrderService orderService, AppDbContext db)
    {
        _orderService = orderService;
        _db = db;
    }

    [HttpPost("")]
    public IActionResult Create()
    {
        var productId = Guid.NewGuid();
        var quantity = 1;

        var order = _orderService.CreateOrder(productId, quantity);

        _db.Orders.Add(order);
        _db.SaveChanges();

        return Ok(order);
    }
}
