using Blinko_5_minute.context;
using Blinko_5_minute.model;

namespace Blinko_5_minute.manager
{
    public class OrderManager
    {
        private DeliveryManager deliveryManager;
        private BlinkoDBContext _context;  
        private DarkStoreManager _darkStoreManager

        public OrderManager(DeliveryManager deliveryManager, BlinkoDBContext context, DarkStoreManager darkStoreManager)
        {
            this.deliveryManager = deliveryManager;
            _context = context;
            _darkStoreManager = darkStoreManager;
        }

        public async Task PlaceOrder(Order order)
        {
            var deliverypartner = await deliveryManager.GetNearestDeliveryPartner();
            order.DeliveryAgent = deliverypartner;


        }


        public Task GetDeliveryAgentList(Order order)
        {
            
        }
    }
}
