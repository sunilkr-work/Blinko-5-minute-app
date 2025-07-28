using Blinko_5_minute.inventoryStore;
using Blinko_5_minute.replineshStrategy;

namespace Blinko_5_minute.manager
{
    public class InventoryManager
    {
        private IInventoryStore _inventoryStore;
        public InventoryManager(IInventoryStore inventoryStore)
        {
            _inventoryStore = inventoryStore;
        }

        public Task CheckInventory(int sku)
        {
            return _inventoryStore.GetAvailableInventory(sku);
        }

        public Task AddInventory(int sku, int quantity)
        {
            return _inventoryStore.AddInventory(sku, quantity);
        }

        public Task RemoveInventory(int sku, int quantity)
        {
            return _inventoryStore.RemoveInventory(sku, quantity);

        }
        public Task<int> GetAvailableInventory(int sku)
        {
            return _inventoryStore.GetAvailableInventory(sku);
        }

        
    }
}
