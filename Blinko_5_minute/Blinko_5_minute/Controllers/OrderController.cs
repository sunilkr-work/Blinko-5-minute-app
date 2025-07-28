using Blinko_5_minute.manager;
using Blinko_5_minute.model;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderManager _orderManager;
        public OrderController(OrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpPost("place-order")]
        public async Task<ActionResult> PlaceOrder([FromBody] Order order)
        {
            try
            {
                await _orderManager.PlaceOrder(order);
                return Ok(new { message = "Order placed successfully." });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error placing order: {ex.Message}", ex);
            }
        }
    }
}
