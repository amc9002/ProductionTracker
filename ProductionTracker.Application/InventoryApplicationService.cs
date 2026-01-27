using ProductionTracker.Domain;

namespace ProductionTracker.Application
{
    public class InventoryApplicationService(
        Inventory inventory,
        InMemoryCatalog catalog)
    {
        private readonly Inventory _inventory = inventory;
        private readonly InMemoryCatalog _catalog = catalog;

        /// <summary>
        /// Registers a product in inventory (creates stock card with zero quantity).
        /// </summary>
        public Result RegisterProduct(Guid productId)
        {
            if (!_catalog.Exists(productId))
                return Result.Invalid("Product does not exist in catalog");

            if (_inventory.HasStockItem(productId))
                return Result.Invalid("Stock item already exists");

            _inventory.CreateStockItem(productId);
            return Result.Success();
        }

        /// <summary>
        /// Receives product into inventory.
        /// Automatically registers stock item if it does not exist.
        /// </summary>
        public Result ReceiveProduct(Guid productId, int amount)
        {
            if (amount <= 0)
                return Result.Invalid("Amount must be greater than zero");

            if (!_catalog.Exists(productId))
                return Result.NotFound("Product does not exist in catalog");

            if (!_inventory.HasStockItem(productId))
            {
                var registerResult = RegisterProduct(productId);
                if (registerResult.Status != ResultStatus.Success)
                    return registerResult;
            }

            _inventory.ReceiveStock(productId, amount);
            return Result.Success();
        }

        /// <summary>
        /// Issues product from inventory.
        /// </summary>
        public Result IssueProduct(Guid productId, int amount)
        {
            if (amount <= 0)
                return Result.Invalid("Amount must be greater than zero");

            if (!_inventory.TryGetAvailableQuantity(productId, out var available))
                return Result.NotFound("Stock item does not exist");

            if (available < amount)
                return Result.Conflict(
                    "Insufficient stock",
                    new { Available = available }
                );

            _inventory.IssueStock(productId, amount);
            return Result.Success();
        }

    }
}
