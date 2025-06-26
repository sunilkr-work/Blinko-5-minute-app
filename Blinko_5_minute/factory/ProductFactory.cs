using Blinko_5_minute.model;

namespace Blinko_5_minute.factory
{
    public interface IProductFactory
    {
        Product CreateProduct();
    }

    public class ProductFactory
    {
        public Product CreateProduct(int sku, string name, double price, string category, int quantity)
        {
            return new Product(sku, name, price, category, quantity);
        }
    }
}
