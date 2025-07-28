using Blinko_5_minute.context;
using Blinko_5_minute.manager;
using Blinko_5_minute.model;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DeliveryAgentController : ControllerBase
    {
        private DeliveryManager _deliveryManager;
        public DeliveryAgentController(DeliveryManager deliveryManager) {
            _deliveryManager = deliveryManager;
        }
        [HttpPost("add-delivery-agent")]
        public async Task<ActionResult> AddDeliveryAgent([FromBody] DeliveryPartener deliveryPartener)
        {
            var deliveryAgent = await _deliveryManager.GetDeliveryPartenerById(deliveryPartener.Id);
            if (deliveryAgent == null)
            {
                await _deliveryManager.AddDeliveryPartener(deliveryPartener);
            }
            else
            {
                return BadRequest(new { message = "Delivery agent already exists." });
            }
            return Ok(new { message = "Delivery agent added successfully." });
        }

        [HttpGet("get-delivery-agent/{id}")]

        public async Task<ActionResult<DeliveryPartener>> GetDeliveryAgent(int id)
        {
            var deliveryAgent = await _deliveryManager.GetDeliveryPartenerById(id);
            if (deliveryAgent == null)
            {
                return NotFound(new { message = "Delivery agent not found." });
            }
            return Ok(deliveryAgent);
        }
    }
}
