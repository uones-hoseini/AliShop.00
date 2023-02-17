using AliShop.Application.Contexts.Interfaces;
using AliShop.Domain.Attributes;
using AliShop.Domain.Catalog;
using AliShop.Domain.Users;
using AliShop.Persistence.EntityConfiguration;
using AliShop.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace AliShop.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogType { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemovedTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
                }
            }
            builder.Entity<CatalogType>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());

            DataBaseContextSeed.CatalogSeed(builder);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedStiets = ChangeTracker.Entries()
                  .Where(p => p.State == EntityState.Modified ||
                  p.State == EntityState.Added ||
                  p.State == EntityState.Deleted
                  );
            foreach (var item in modifiedStiets)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var insertd = entityType.FindProperty("InsertTime");
                var updated = entityType.FindProperty("UpdateTime");
                var removedTime = entityType.FindProperty("RemovedTime");
                var isremoved = entityType.FindProperty("IsRemoved");

                if (item.State == EntityState.Added && insertd != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Modified && updated != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && removedTime != null && isremoved != null)
                {
                    item.Property("RemovedTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                    item.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }
    }
}
