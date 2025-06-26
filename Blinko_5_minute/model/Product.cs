namespace Blinko_5_minute.model
{
    public class Product
    {
        public int Sku { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public int quantity { get; set; }

        public Product(int id, string name, double price, string category, int quantity)
        {
            Sku = id;
            Name = name;
            Price = price;
            Category = category;
            this.quantity = quantity;
        }




    }
}
