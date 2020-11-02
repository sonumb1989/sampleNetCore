using Common.Entities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Database
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DetailImage> DetailImages { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
    }
}
