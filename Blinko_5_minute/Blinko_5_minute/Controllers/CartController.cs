using Blinko_5_minute.context;
using Blinko_5_minute.manager;
using Blinko_5_minute.model;
using Microsoft.AspNetCore.Mvc;


namespace Blinko_5_minute.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CartController : Controller
    {
        private BlinkoDBContext _blinkoDBContext;
        private Cart _cart;

        public CartController(BlinkoDBContext context, Cart cart)
        {
            _blinkoDBContext = context;
            _cart = cart;
        }

        [HttpPost("add-product")]
        public async Task<ActionResult> AddProduct(int id, int quantity)
        {
          var product = await _blinkoDBContext.CartItems.FindAsync(id);

           if(product == null)
            {
                _blinkoDBContext.CartItems.Add(new model.CartItem
                {
                    CartId = _cart.CartId,
                    ProductId = id,
                    Quantity = quantity,
                });
            }
           else
            {
                product.Quantity += quantity;
            }
           await _blinkoDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete-product/{id}/{quantity}")]
        public async Task<ActionResult> RemoveProduct(int id, int quantity)
        {
            var product =  _blinkoDBContext.CartItems.FirstOrDefault(x=> x.ProductId == id ) ;
            product.Quantity -= quantity;

            if (product.Quantity <= 0)
            {
                _blinkoDBContext.CartItems.Remove(product);
            }
            await _blinkoDBContext.SaveChangesAsync();
            return Ok(new { message = "Removed to cart successfully." });
        }
    }
}
