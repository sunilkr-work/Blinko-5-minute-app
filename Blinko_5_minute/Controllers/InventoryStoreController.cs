using Blinko_5_minute.manager;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController]
    public class InventoryStoreController: ControllerBase
    {
        private InventoryManager _inventoryManager;

        public InventoryStoreController(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddInventory(int Sku, int quantity)
        {
            await _inventoryManager.AddInventory(Sku, quantity);
            return Ok(new { message = "Inventory added successfully." });
        }

        [HttpPut]
        public async Task<ActionResult> RemoveInventory(int Sku, int quantity)
        {
            await _inventoryManager.RemoveInventory(Sku, quantity);
            return Ok();
        }

        public async Task<ActionResult<int>> GetAvailableinventory( int Sku)
        {
           int result = await _inventoryManager.GetAvailableInventory(Sku);
            return result;
        }
    }
}
