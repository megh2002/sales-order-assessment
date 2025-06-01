using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SalesOrderAssessment.Domain;
using SalesOrderAssessment.Domain.Entities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SalesOrderAssessment.Services
{
    public class CommonService : ICommonService
    {
        private readonly SalesOrderAssessmentDbContext _context;

        public CommonService(SalesOrderAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task ReadCSVFile(IFormFile file)
        {
            var customers = new List<Customer>();
            var products = new List<Product>();
            var orders = new List<Order>();
            var orderItems = new List<OrderItem>();
            var errors = new List<string>();

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            try
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    try
                    {
                        var customerId = csv.GetField("Customer ID")?.Trim();
                        var customerName = csv.GetField("Customer Name")?.Trim();
                        var customerEmail = csv.GetField("Customer Email")?.Trim();
                        var customerAddress = csv.GetField("Customer Address")?.Trim();

                        var productId = csv.GetField("Product ID")?.Trim();
                        var productName = csv.GetField("Product Name")?.Trim();
                        var productCategory = csv.GetField("Category")?.Trim();
                        var productPrice = csv.GetField("Unit Price")?.Trim();
                        int unitPrice = 0;

                        if (productPrice != null)
                            _ = int.TryParse(productPrice, out unitPrice);

                        var orderId = csv.GetField("Order ID")?.Trim();
                        var dateOfSale = csv.GetField("Date of Sale")?.Trim();
                        var shippingCost = csv.GetField("Shipping Cost")?.Trim();
                        var paymentMethod = csv.GetField("Payment Method")?.Trim();
                        DateTime saleDate = DateTime.Now;
                        decimal costOfShipping = 0;
                        if (dateOfSale is not null)
                            _ = DateTime.TryParse(dateOfSale, out saleDate);

                        if (shippingCost is not null)
                            _ = decimal.TryParse(shippingCost, out costOfShipping);

                        var quantitySold = csv.GetField("Quantity Sold")?.Trim();
                        var discount = csv.GetField("Discount")?.Trim();
                        int soldQuantity = 0;
                        decimal calculatedDiscount = 0;
                        if (quantitySold is not null)
                            _ = int.TryParse(quantitySold, out soldQuantity);

                        if (discount is not null)
                            _ = decimal.TryParse(discount, out calculatedDiscount);

                        customers.Add(new Customer()
                        {
                            Id = customerId ?? "",
                            Name = customerName ?? "",
                            Email = customerEmail ?? "",
                            Address = customerAddress ?? ""
                        });

                        products.Add(new Product()
                        {
                            Id = productId ?? "",
                            Name = productName ?? "",
                            Category = productCategory ?? "",
                            UnitPrice = unitPrice
                        });

                        orders.Add(new Order()
                        {
                            Id = orderId ?? "",
                            Customer_Id = customerId ?? "",
                            DateOfSale = saleDate,
                            ShippingCost = costOfShipping,
                            PaymentMethod = paymentMethod ?? ""
                        });

                        orderItems.Add(new OrderItem()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Order_Id = orderId ?? "",
                            Product_Id = productId ?? "",
                            QuantitySold = soldQuantity,
                            Discount = calculatedDiscount
                        });
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Row {csv.Context}: {ex.Message}");
                    }
                }

                await _context.Customers.AddRangeAsync(customers);
                await _context.Products.AddRangeAsync(products);
                await _context.Orders.AddRangeAsync(orders);
                await _context.OrderItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
