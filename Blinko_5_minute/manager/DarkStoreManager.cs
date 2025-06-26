using Blinko_5_minute.context;
using Blinko_5_minute.darkStore;
using Blinko_5_minute.model;
using Blinko_5_minute.replineshStrategy;
using System.Runtime.CompilerServices;

namespace Blinko_5_minute.manager
{
    public class DarkStoreManager
    {
        public DarkStore darkStore;
        string name;
        public IReplenishStrategy _replenishStrategy;
        private DeliveryManager _deliveryManager;
        public BlinkoDBContext _blinkoDBContext;
        //public static DarkStoreManager Instance;

        //private DarkStoreManager()
        //{
        //    // Private constructor to prevent instantiation without parameters
        //}

        //public static  DarkStoreManager GetInsance()
        //{
        //    if (Instance == null)
        //    {
        //        Instance = new DarkStoreManager();
        //    }
        //    return Instance;
        //}
        public DarkStoreManager(DarkStore darkStore, string name, IReplenishStrategy replenishStrategy, DeliveryManager deliveryManager, BlinkoDBContext blinkoDBContext)
        {
            this.darkStore = darkStore;
            this.name = name;
            this._replenishStrategy = replenishStrategy;
            this._deliveryManager = deliveryManager;
            _blinkoDBContext = blinkoDBContext;

        }

        public double GetDistance(double x, double y)
        {
            return darkStore.GetDistance(x, y);
        }

        public async Task RunReplenishWeekly( Dictionary<int,int> items, InventoryManager inventoryManager)
        {
            _replenishStrategy.WeeklyReplenish(items, inventoryManager);
        }

        public async Task RunReplenish(Dictionary<int, int> items, InventoryManager inventoryManager)
        {
            _replenishStrategy.Replenish(items, inventoryManager);
        }

        public async Task ExecuteOrder(Order order)
        {
            var partner = await _deliveryManager.GetNearestDeliveryPartner();
            var darkStore = await GetNearestDarkStore(order.Customer.X_Cord, order.Customer.Y_Cord);
            if (partner == null)
            {
                throw new Exception();
            }
         //   _deliveryManager.DarkStore = darkStore;

            order.DeliveryAgent = partner;
        }

        public async Task<DarkStore> GetNearestDarkStore(double x,double y)
        {
            double nearestDistance = double.MaxValue;
            DarkStore nearestDarkStore = null;

           var stores = _blinkoDBContext.DarkStores.ToList();

            foreach (var store in stores)
            {
                var distance = Math.Sqrt(Math.Pow(store._Xcord - x, 2) + Math.Pow(store._Ycord - y, 2));

                if(distance < nearestDistance)
                {
                    nearestDarkStore = store;
                    nearestDistance = distance;
                }
            }
            return nearestDarkStore;
        }
    }
}
