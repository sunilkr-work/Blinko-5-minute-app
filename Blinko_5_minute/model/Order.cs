namespace Blinko_5_minute.model
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public double TotalAmount { get; set; }
        public DeliveryPartener DeliveryAgent { get; internal set; }

        public Customer Customer { get; internal set; }

        public Order(int id, string name, string desc, string address, double total, Customer customer)
        {
            Id = id;
            Name = name;
            Description = desc;
            Address = address;  
            TotalAmount = total;
            Customer = customer;



        }


    }
}
