using Blinko_5_minute.manager;
using Blinko_5_minute.replineshStrategy;

namespace Blinko_5_minute
{

    public interface IBlinkoFacade
    {
        DarkStoreManager _darkStoreManager { get; set; }
        DeliveryManager _deliveryManager { get; set; }
        OrderManager _orderManager { get; set; }
        InventoryManager _inventoryManager { get; set; }
        IReplenishStrategy _replenishStrategy { get; set; }


    }


    public class BlinkoFacade
    {

    }
}
