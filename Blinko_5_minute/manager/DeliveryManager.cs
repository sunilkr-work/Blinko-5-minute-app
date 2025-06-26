using Blinko_5_minute.context;
using Blinko_5_minute.darkStore;
using Blinko_5_minute.model;

namespace Blinko_5_minute.manager
{
    public class DeliveryManager
    {

        private DeliveryPartener _deliveryPartener;
        private BlinkoDBContext _blinkoDBcontext;

        private DarkStore _darkStore;

        public DeliveryManager(DeliveryPartener deliveryPartener, BlinkoDBContext blinkoDBContext, DarkStore darkStore)
        {
            _deliveryPartener = deliveryPartener;
            _blinkoDBcontext = blinkoDBContext;
            _darkStore = darkStore;
        }   
        public Task ScheduleDelivery(Order order, string address)
        {
            // Logic to schedule delivery for the order
            // This could involve interacting with a delivery service API or database
            var partner = GetNearestDeliveryPartner();
            order.
            return Task.CompletedTask;
        }

        public Task CancelDelivery(int orderId)
        {
            // Logic to cancel the scheduled delivery for the order
            return Task.CompletedTask;
        }

        public Task TrackDelivery(int orderId)
        {
            // Logic to track the delivery status of the order
            return Task.CompletedTask;
        }

        public async Task<DeliveryPartener>  GetNearestDeliveryPartner()
        {
            // Logic to calculate the distance from the user to the delivery partner
            // This could involve using a mapping service or formula to calculate distance
            // For simplicity, let's assume we return a dummy value

            var availableDeliveryParteners = _blinkoDBcontext.DeliveryParteners.Where(dp => dp.IsAvailable).ToList();

            if (availableDeliveryParteners.Count == 0)
            {
                throw new Exception("No available delivery partners found.");
            }

            int nearestPartener=int.MaxValue;
            DeliveryPartener nearestDeliveryPartener = null;

            foreach (var  partner in availableDeliveryParteners)
            {
                double distance = Math.Sqrt(Math.Pow(partner.X_Cord - _darkStore._Xcord, 2) + Math.Pow(partner.Y_Cord - _darkStore._Ycord, 2));
                if(distance < nearestPartener)
                {
                    nearestDeliveryPartener = partner;
                    nearestPartener = (int)distance;
                }

            }


            return nearestDeliveryPartener;
        }
    }
}
