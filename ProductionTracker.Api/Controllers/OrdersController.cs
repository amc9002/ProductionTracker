using Microsoft.AspNetCore.Mvc;

using ProductionTracker.Application;
using ProductionTracker.Application.Requests;
using ProductionTracker.Domain;
using ProductionTracker.Infrastructure;

namespace ProductionTracker.Api.Controllers;

/// <summary>
/// Manages inventory operations through an order-based workflow.
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderApplicationService _orderService;
    private readonly AppDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/>.
    /// </summary>
    public OrdersController(OrderApplicationService orderService, AppDbContext db)
    {
        _orderService = orderService;
        _db = db;
    }

    /// <summary>
    /// Creates and executes a new inventory order.
    /// </summary>
    /// <param name="request">The order details including product ID, action type, and quantity.</param>
    /// <returns>The processed order with its final status (Completed or Rejected).</returns>
    /// <response code="200">The order was processed successfully.</response>
    /// <response code="400">The request failed due to business rule violations (e.g., insufficient stock).</response>
    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateOrder([FromBody] OrderRequest request)
    {
        try
        {
            // Execute the business logic via the application service
            var order = _orderService.ExecuteRequest(request);

            // Persist the order history in the database
            _db.Orders.Add(order);
            _db.SaveChanges();

            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            // Return a meaningful error message to the client
            return BadRequest(new { message = ex.Message });
        }
    }
}
