using Microsoft.EntityFrameworkCore;
// using OrderingApplication.Data;
using OrderingDomain.Models;
using System.Reflection;

namespace OrderingInfrastructure.Data;
public class ApplicationDbContext : DbContext
// IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // it scan what all configurations are there in the assembly(in a entity or db table) and apply those configurations like constraints, keys, relationships,column etc.
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}