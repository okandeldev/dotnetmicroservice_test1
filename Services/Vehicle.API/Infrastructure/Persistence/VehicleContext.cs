 
using Microsoft.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Core.Entities;
using VehicleAPI.Core.Domain.Entities;

namespace VehicleAPI.Infrastructure.Persistence
{
    public class VehicleContext :DbContext
    {

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditOnCreateEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "";
                        break; 
                }
            }
            foreach (var entry in ChangeTracker.Entries<AuditOnUpdateEntity>())
            {
                switch (entry.State)
                { 
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
