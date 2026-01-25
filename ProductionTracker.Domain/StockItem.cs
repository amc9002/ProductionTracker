using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionTracker.Domain
{
    class StockItem
    {
        private readonly Guid _productId;
        private int _quantity;

        public StockItem(Guid id, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(quantity);

            _productId = id;
            _quantity = quantity;
        }

        public void Add(int amount)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(amount);
            _quantity += amount;
        }

        public void Issue(int amount)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(amount);
            if (_quantity < amount)
                throw new InvalidOperationException("Amount is not enough");
            _quantity -= amount;
        }

        public Guid ProductId => _productId;
        public int Quantity => _quantity;
    }
}
