using Blinko_5_minute.context;
using Blinko_5_minute.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blinko_5_minute.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private BlinkoDBContext _blinkoDBContext;
        private Product _product;

        public ProductController(BlinkoDBContext context, Product product)
        {
            _blinkoDBContext = context;
            _product = product;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult> AddProduct([FromBody] Product item)
        {
            var product = await _blinkoDBContext.Products.FindAsync(item.Sku);

            if (product == null)
            {
               await _blinkoDBContext.Products.AddAsync(item);
            }
            await _blinkoDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
