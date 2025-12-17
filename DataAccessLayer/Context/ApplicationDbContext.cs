using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<StockUnit> StockUnits { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockType>().HasData(SeedData.StockTypes);
            modelBuilder.Entity<StockUnit>().HasData(SeedData.StockUnits);
            modelBuilder.Entity<StockItem>().HasData(SeedData.StockItems);

            modelBuilder.Entity<StockType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<StockUnit>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<StockItem>()
                .HasIndex(x => x.StockUnitId)
                .IsUnique();

            modelBuilder.Entity<StockType>()
                .HasMany(x => x.StockUnits)
                .WithOne(x => x.StockType)
                .HasForeignKey(x => x.StockTypeId);

            modelBuilder.Entity<StockUnit>()
                .HasMany(x => x.StockItems)
                .WithOne(x => x.StockUnit)
                .HasForeignKey(x => x.StockUnitId);

            modelBuilder.Entity<RequestLog>()
                .HasIndex(x => x.CreatedDate);

            modelBuilder.Entity<RequestLog>()
                .HasIndex(x => x.CorrelationId);

            modelBuilder.Entity<RequestLog>()
                .HasIndex(x => x.StatusCode);

        }
    }
}
