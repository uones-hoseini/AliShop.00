using AliShop.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Domain.Catalog
{
    [Auditable]
    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}
