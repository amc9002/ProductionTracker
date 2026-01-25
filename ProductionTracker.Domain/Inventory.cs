using ProductionTracker.Domain.ProductionTracker.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionTracker.Domain
{
    class Inventory
    {
        private readonly List<StockItem> stockItems = [];
            
        public void Add(Guid productId, int amount)
        {
            var item = stockItems.Find(it => it.ProductId == productId);
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
            var item = stockItems.Find(it => it.ProductId == productId) 
                ?? throw new ArgumentException("No such item");

            item.Issue(amount);
        }

        public void RemoveStockItem(Guid productId)
        {
            var itemId = stockItems.Find(item => item.ProductId == productId);
            if (itemId == null) throw new ArgumentException("No such item");

            stockItems.Remove(item => item.ProductId == productId);
        }
    }
}
