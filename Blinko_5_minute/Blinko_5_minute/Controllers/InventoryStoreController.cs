using Blinko_5_minute.manager;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryStoreController: ControllerBase
    {
        private InventoryManager _inventoryManager;

        public InventoryStoreController(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpPost("add-inventory")]
        public async Task<ActionResult> AddInventory(int Sku, int quantity)
        {
            await _inventoryManager.AddInventory(Sku, quantity);
            return Ok(new { message = "Inventory added successfully." });
        }

        [HttpDelete("delete-inventory/{Sku}/{quantity}")]
        public async Task<ActionResult> RemoveInventory(int Sku, int quantity)
        {
            await _inventoryManager.RemoveInventory(Sku, quantity);
            return Ok();
        }

        [HttpGet("get-available-inventory/{Sku}")]
        public async Task<ActionResult<int>> GetAvailableinventory( int Sku)
        {
           int result = await _inventoryManager.GetAvailableInventory(Sku);
            return result;
        }
    }
}
