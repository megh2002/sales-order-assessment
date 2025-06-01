using Microsoft.EntityFrameworkCore;
using SalesOrderAssessment.Domain.Entities;
using System;

namespace SalesOrderAssessment.Domain
{
    public class SalesOrderAssessmentDbContext : DbContext
    {
        public SalesOrderAssessmentDbContext(DbContextOptions<SalesOrderAssessmentDbContext> options) : base(options) { }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }
}
