using ProductionTracker.Domain;

namespace ProductionTracker.Application
{

    public class InventoryApplicationService(Inventory inventory)
    {
        public Inventory Inventory { get; } = inventory;

        public Result AddToInventory(Guid id, int amount)
        {
            if (amount < 0)
                return new Result(ResultStatus.Invalid,
                    "Amount must not be less than 0",
                    null);

            Inventory.Add(id, amount);
            return new Result(ResultStatus.Success,
                "Product added successfully",
                null
                );

        }
    }
}
