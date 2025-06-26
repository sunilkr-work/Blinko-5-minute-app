using Blinko_5_minute.context;
using Blinko_5_minute.model;

namespace Blinko_5_minute.inventoryStore
{
    public class DbInventoryStore : IInventoryStore
    {

        private BlinkoDBContext _context;
        public DbInventoryStore(BlinkoDBContext context)
        {
            _context = context;
        }
        public async Task<int> GetAvailableInventory(int sku)
        {
           var product = _context.Products.Find(sku);
            if(product != null)
            {
                return product.quantity;
            }
            return 0;
        }
        public async Task AddInventory(int sku, int quantity)
        {
            var product = _context.Products.Find(sku);
            if (product != null)
            {
                product.quantity += quantity;
                _context.SaveChanges();
            }
        }

        public async Task RemoveInventory(int sku, int quantity)
        {
            var product = _context.Products.Find(sku);
            if (product != null)
            {
                product.quantity -= quantity;
                _context.SaveChanges();
            }
        }
    }
}
