using ProductionTracker.Domain;
using ProductionTracker.Application.Requests;

namespace ProductionTracker.Application
{
    /// <summary>
    /// Creates operational orders from incoming request data
    /// and delegates their execution to inventory.
    /// </summary>
    public class OrderApplicationService
    {
        private readonly InventoryApplicationService _inventoryService;

        public OrderApplicationService(
            InventoryApplicationService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Creates an operational order based on request data
        /// and passes it to inventory for execution.
        /// </summary>
        public Order CreateOrder(OrderRequest request)
        {
            // 1. Пераклад DTO → аперацыйны загад
            var order = new Order(
                request.Action,
                request.ProductId,
                request.Quantity);

            // 2. Перадача загада выканаўцу
            _inventoryService.Execute(order);

            // 3. Вяртаем загад з адзнакай аб выкананні
            return order;
        }
    }
}
