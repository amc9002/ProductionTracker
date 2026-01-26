namespace ProductionTracker.Domain
{
    public class Inventory
    {
        private readonly List<StockItem> stockItems = [];
            
        public void Add(Guid productId, int amount)
        {
            var item = stockItems
                .SingleOrDefault(it => it.ProductId == productId);
            if (item == null)
            {
                var stockItem = new StockItem(productId, amount);
                stockItems.Add(stockItem);
            }
            else
            {
                item.Add(amount);
            }
                
        }
        public void Issue(Guid productId, int amount)
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
