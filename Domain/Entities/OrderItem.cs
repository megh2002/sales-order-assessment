namespace SalesOrderAssessment.Domain.Entities
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string Order_Id { get; set; }
        public string Product_Id { get; set; }
        public int QuantitySold { get; set; }
        public decimal Discount { get; set; }
        public Order Order { get; set; } 
        public Product Product { get; set; }
    }
}
