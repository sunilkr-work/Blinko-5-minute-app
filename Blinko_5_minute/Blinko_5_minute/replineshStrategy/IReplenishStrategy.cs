using Blinko_5_minute.manager;

namespace Blinko_5_minute.replineshStrategy
{
    public interface IReplenishStrategy
    {
        public int ThresHold { get; }
        InventoryManager InventoryManager { get; set; }

        Task Replenish(Dictionary<int,int> item, InventoryManager inventoryManager);

        Task WeeklyReplenish(Dictionary<int, int> items, InventoryManager inventoryManager);
       
    }
}
