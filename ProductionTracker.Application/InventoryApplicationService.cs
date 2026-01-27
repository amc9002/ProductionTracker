using ProductionTracker.Domain;

namespace ProductionTracker.Application
{
    /// <summary>
    /// Executes operational orders against inventory.
    /// </summary>
    public class InventoryApplicationService(
        Inventory inventory,
        InMemoryCatalog catalog)
    {
        private readonly Inventory _inventory = inventory;
        private readonly InMemoryCatalog _catalog = catalog;

        /// <summary>
        /// Executes the given operational order.
        /// Sets order status according to execution result.
        /// </summary>
        public void Execute(Order order)
        {
            try
            {
                switch (order.Action)
                {
                    case OrderAction.Register:
                        RegisterProduct(order.ProductId);
                        break;

                    case OrderAction.Receive:
                        ReceiveProduct(order.ProductId, order.Quantity);
                        break;

                    case OrderAction.Issue:
                        IssueProduct(order.ProductId, order.Quantity);
                        break;

                    default:
                        throw new InvalidOperationException("Unknown order action");
                }

                order.MarkCompleted();
            }
            catch
            {
                order.MarkRejected();
            }
        }

        /// <summary>
        /// Registers a product in inventory (creates stock card with zero quantity).
        /// </summary>
        private void RegisterProduct(Guid productId)
        {
            if (!_catalog.Exists(productId))
                throw new InvalidOperationException("Product does not exist in catalog");

            if (_inventory.HasStockItem(productId))
                throw new InvalidOperationException("Stock item already exists");

            _inventory.CreateStockItem(productId);
        }

        /// <summary>
        /// Receives product into inventory.
        /// Automatically registers stock item if it does not exist.
        /// </summary>
        private void ReceiveProduct(Guid productId, int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            if (!_catalog.Exists(productId))
                throw new InvalidOperationException("Product does not exist in catalog");

            if (!_inventory.HasStockItem(productId))
            {
                RegisterProduct(productId);
            }

            _inventory.ReceiveStock(productId, amount);
        }

        /// <summary>
        /// Issues product from inventory.
        /// </summary>
        private void IssueProduct(Guid productId, int amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            if (!_inventory.TryGetAvailableQuantity(productId, out var available))
                throw new InvalidOperationException("Stock item does not exist");

            if (available < amount)
                throw new InvalidOperationException("Insufficient stock");

            _inventory.IssueStock(productId, amount);
        }
    }
}
