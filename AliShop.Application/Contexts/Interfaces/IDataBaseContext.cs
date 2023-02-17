using AliShop.Domain.Catalog;
using AliShop.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliShop.Application.Contexts.Interfaces
{
    public interface IDataBaseContext
    {
        DbSet<CatalogBrand> CatalogBrands { get; set; }
        DbSet<CatalogType> CatalogType { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
