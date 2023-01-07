namespace VehicleAPI.Core.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string ChassisNumber { get; set; }
        public string Model { get; set; }
        public string Color { get; set; } 
        public HashSet<string> Features { get; set; } 
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
