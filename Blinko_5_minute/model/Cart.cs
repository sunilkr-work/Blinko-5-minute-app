namespace Blinko_5_minute.model
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CartItem> items { get; set; } = new List<CartItem>();

    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; } = null!;
    }
}
