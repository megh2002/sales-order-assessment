namespace SalesOrderAssessment.Domain.Entities
{
    public class Order
    {
        public string Id { get; set; } 
        public string Customer_Id { get; set; } 
        public DateTime DateOfSale { get; set; }
        public decimal ShippingCost { get; set; }
        public string PaymentMethod { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
