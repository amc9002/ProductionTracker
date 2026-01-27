namespace ProductionTracker.Domain
{
    public class Inventory
    {
        private readonly List<StockItem> stockItems = [];

        public bool HasStockItem(Guid productId)
        => stockItems.Any(it => it.ProductId == productId);

        public bool TryGetAvailableQuantity(Guid productId, out int quantity)
        {
            var item = stockItems.SingleOrDefault(it => it.ProductId == productId);
            if (item == null)
            {
                quantity = 0;
                return false;
            }

            quantity = item.Quantity;
            return true;
        }

        public void CreateStockItem(Guid productId)
        {
            if (stockItems.Any(it => it.ProductId == productId))
                throw new InvalidOperationException("Stock item already exists");

            stockItems.Add(new StockItem(productId, 0));
        }

        public void ReceiveStock(Guid productId, int amount)
        {
            var item = stockItems
                .SingleOrDefault(it => it.ProductId == productId)
                ?? throw new InvalidOperationException("Stock item does not exist");

            item.Add(amount);
        }

        public void IssueStock(Guid productId, int amount)
        {
            var item = stockItems
                .SingleOrDefault(it => it.ProductId == productId) 
                ?? throw new ArgumentException("No such item");

            item.Issue(amount);
        }

        public void RemoveStockItem(Guid productId)
        {
            var item = stockItems
                .SingleOrDefault(item => item.ProductId == productId) 
                ?? throw new ArgumentException("No such item");

            stockItems.Remove(item);
        }
    }
}
