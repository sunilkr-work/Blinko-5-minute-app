using Blinko_5_minute.model;

namespace Blinko_5_minute.model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public double X_Cord { get; set; }
        public double Y_Cord { get; set; }

        public Cart Cart { get; set; }
       
    }
}
