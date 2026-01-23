using Microsoft.AspNetCore.Mvc;

using ProductionTracker.Application;

namespace ProductionTracker.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("")]
    public IActionResult Create()
    {
        var productId = Guid.NewGuid();
        var quantity = 1;

        var order = _orderService.CreateOrder(productId, quantity);

        return Ok(order);
    }
}
