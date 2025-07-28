using Blinko_5_minute.context;
using Blinko_5_minute.model;

namespace Blinko_5_minute.manager
{
    public class OrderManager
    {
        private DeliveryManager _deliveryManager;
        private BlinkoDBContext _context;
        private DarkStoreManager _darkStoreManager;

        public OrderManager(DeliveryManager deliveryManager, BlinkoDBContext context, DarkStoreManager darkStoreManager)
        {
            _deliveryManager = deliveryManager;
            _context = context;
            _darkStoreManager = darkStoreManager;
        }

        public async Task PlaceOrder(Order order)
        {
           // var deliverypartner = await _deliveryManager.GetNearestDeliveryPartner();
            var darkStore = await _darkStoreManager.GetNearestDarkStore(order.Customer.X_Cord, order.Customer.Y_Cord);
            var deliverypartner = await _deliveryManager.GetNearestDeliveryPartner(darkStore);
            order.DeliveryAgent = deliverypartner;
            await _darkStoreManager.ExecuteOrder(order);
        }
    }
}
