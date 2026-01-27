using ProductionTracker.Domain;

namespace ProductionTracker.Application.Requests
{
    /// <summary>
    /// Request data for creating an operational order.
    /// </summary>
    public class OrderRequest
    {
        /// <summary>
        /// Action to be performed on the product.
        /// </summary>
        public OrderAction Action { get; init; }

        /// <summary>
        /// Identifier of the target product.
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// Quantity involved in the operation.
        /// Meaning depends on the specified action.
        /// </summary>
        public int Quantity { get; init; } = 0;
    }
}
