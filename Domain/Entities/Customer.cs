namespace SalesOrderAssessment.Domain.Entities
{
    public class Customer
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
