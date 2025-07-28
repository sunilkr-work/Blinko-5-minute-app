using Blinko_5_minute.context;
using Blinko_5_minute.darkStore;
using Blinko_5_minute.model;

namespace Blinko_5_minute.manager
{
    public class DeliveryManager
    {

        private BlinkoDBContext _blinkoDBcontext;
        public DeliveryManager(BlinkoDBContext blinkoDBContext)
        {
            _blinkoDBcontext = blinkoDBContext;
        }   
        //public Task ScheduleDelivery(Order order, string address)
        //{
        //   // var partner = GetNearestDeliveryPartner();
        //    order.DeliveryAgent = partner.Result;
        //    return Task.CompletedTask;
        //}

        public Task CancelDelivery(int orderId)
        {
            return Task.CompletedTask;
        }

        public Task TrackDelivery(int orderId)
        {
            return Task.CompletedTask;
        }

        public async Task<DeliveryPartener>  GetNearestDeliveryPartner(DarkStore _darkStore)
        {
            var availableDeliveryParteners = _blinkoDBcontext.DeliveryParteners.Where(dp => dp.IsAvailable).ToList();

            if (availableDeliveryParteners.Count == 0)
            {
                throw new Exception("No available delivery partners found.");
            }

            int nearestPartner=int.MaxValue;
            DeliveryPartener nearestDeliveryPartner = null;

            foreach (var  partner in availableDeliveryParteners)
            {
                double distance = Math.Sqrt(Math.Pow(partner.X_Cord - _darkStore._Xcord, 2) + Math.Pow(partner.Y_Cord - _darkStore._Ycord, 2));
                if(distance < nearestPartner)
                {
                    nearestDeliveryPartner = partner;
                    nearestPartner = (int)distance;
                }
            }

            return nearestDeliveryPartner;
        }

        public async Task<DeliveryPartener> GetDeliveryPartenerById(int id)
        {
            var deliveryPartener = _blinkoDBcontext.DeliveryParteners.FirstOrDefault(dp => dp.Id == id);
            //if (deliveryPartener == null) {
            //    throw new Exception($"Delivery partner with ID {id} not found.");
            //}
            return deliveryPartener;
        }

        public async Task AddDeliveryPartener(DeliveryPartener deliveryPartener)
        {
            if (deliveryPartener == null)
            {
                throw new ArgumentNullException(nameof(deliveryPartener), "Delivery partner cannot be null.");
            }

            _blinkoDBcontext.DeliveryParteners.Add(deliveryPartener);
            await _blinkoDBcontext.SaveChangesAsync();
        }
    }
}
