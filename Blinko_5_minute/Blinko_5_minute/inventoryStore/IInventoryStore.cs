using Blinko_5_minute.model;

namespace Blinko_5_minute.inventoryStore
{
    public interface IInventoryStore
    {
       // int CheckInventory(int sku);
        Task AddInventory(int sku, int quantity);
        Task RemoveInventory(int sku, int quantity);
        Task<int> GetAvailableInventory(int sku);
    }
}
