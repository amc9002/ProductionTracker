namespace ProductionTracker.Domain;

// NOTE:
// Order is an operational command.
// It contains no business intent and no execution logic.
// Interpretation and execution are responsibilities of the application layer.

/// <summary>
/// Represents operational action to be performed on the product
/// </summary>
public enum OrderAction
{
    Issue,      
    Receive,    
    Register    
}

/// <summary>
/// Represents an operational order — a concrete command to change
/// the state of a product in inventory.
/// </summary>
public enum OrderStatus
{
    Created,
    Completed,
    Rejected
}

/// <summary>
/// Operational order that describes a single, concrete inventory action.
///
/// The order does not contain any business logic and does not decide
/// how or why the operation is performed.
/// Its lifecycle is controlled by the application layer.
/// </summary>
public class Order
{
    public Guid Id { get; }
    public OrderAction Action { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Creates a new operational order in the Created state.
    /// </summary>
    /// <param name="action">Operational action to be performed on the product.</param>
    /// <param name="productId">Target product identifier.</param>
    /// <param name="quantity">Quantity for the operation.</param>
    public Order(OrderAction action, Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        Action = action;
        ProductId = productId;
        Quantity = quantity;
        Status = OrderStatus.Created;
    }

    /// <summary>
    /// Marks the order as successfully completed.
    /// This method must be called by the application layer after execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the order has already been processed.
    /// </exception>
    public void MarkCompleted()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Order already processed");

        Status = OrderStatus.Completed;
    }

    /// <summary>
    /// Marks the order as rejected.
    /// This method must be called by the application layer if execution fails.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the order has already been processed.
    /// </exception>
    public void MarkRejected()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Order already processed");

        Status = OrderStatus.Rejected;
    }
}
