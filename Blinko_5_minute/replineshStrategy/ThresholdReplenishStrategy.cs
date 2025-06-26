using Blinko_5_minute.manager;

namespace Blinko_5_minute.replineshStrategy
{
    public class ThresholdReplenishStrategy : IReplenishStrategy
    {
        public int ThreshHold;
        public InventoryManager InventoryManager { get; set; }

        public ThresholdReplenishStrategy(int threshold, InventoryManager inventoryManager)
        {
            ThreshHold = threshold;
            InventoryManager = inventoryManager;
        }

        public async Task Replenish(Dictionary<int, int> items, InventoryManager inventoryManager)
        {
            foreach (var item in items)
            {
                int availableInventory = await inventoryManager.GetAvailableInventory(item.Key);
                if (availableInventory < ThreshHold)
                {
                    int replenishAmount = ThreshHold - availableInventory;
                    await inventoryManager.AddInventory(item.Key, replenishAmount);
                }
            }
        }

        public async Task WeeklyReplenish(Dictionary<int, int> items, InventoryManager inventoryManager)
        {
            foreach (var item in items)
            {
                int availableInventory = await inventoryManager.GetAvailableInventory(item.Key);
                await inventoryManager.AddInventory(item.Key, item.Value);
            }
        }


    }
}
