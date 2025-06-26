namespace Blinko_5_minute.model
{
    public class DeliveryPartener
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public double X_Cord { get; set; }
        public double Y_Cord { get; set; }
        public Guid DarkStoreId { get; set; }
        public bool IsAvailable { get; set; } 

        public DeliveryPartener( int id , string name, string phoneNumber, string address, double x ,double y, Guid darkId )
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            IsActive = true; 
            IsAvailable = true; 
            Address = address;
            X_Cord = x;
            Y_Cord = y;
            DarkStoreId = darkId;
        }
    }
}
