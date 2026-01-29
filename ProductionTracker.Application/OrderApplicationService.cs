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
        private readonly InMemoryCatalog _catalog;

        public OrderApplicationService(
            InventoryApplicationService inventoryService, InMemoryCatalog catalog)
        {
            _inventoryService = inventoryService;
            _catalog = catalog;
        }

        /// <summary>
        /// Creates an operational order based on request data
        /// and passes it to inventory for execution.
        /// </summary>
        public Order ExecuteRequest(OrderRequest request)
        {
            if (!_catalog.Exists(request.ProductId))
            {
                // Калі няма — па тваёй логіцы мы павінны яго дадаць.
                // Але пакуль у OrderRequest толькі ID, а для каталога трэба Name.
                // Давай пакуль кідаць памылку, альбо абмяркуем, адкуль браць імя.
                throw new Exception("Product not found in Catalog. Please register it first.");
            }

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
