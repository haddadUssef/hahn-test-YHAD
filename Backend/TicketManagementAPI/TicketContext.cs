using Microsoft.EntityFrameworkCore;
using TicketManagementAPI.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace TicketManagementAPI
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    var converter = new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),   // Convert to UTC before saving
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Read back as UTC

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(property.Name)
                        .HasConversion(converter);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
