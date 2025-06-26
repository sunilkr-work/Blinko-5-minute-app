using Blinko_5_minute.manager;

namespace Blinko_5_minute.replineshStrategy
{
    public interface IReplenishStrategy
    {
        int ThreshHold { get; }
        InventoryManager InventoryManager { get; set; }
        //int ReplinishAmount;

        Task Replenish(Dictionary<int,int> item, InventoryManager inventoryManager);

        Task WeeklyReplenish(Dictionary<int, int> items, InventoryManager inventoryManager);
       
    }
}
