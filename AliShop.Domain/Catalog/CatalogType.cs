using AliShop.Domain.Attributes;
using System.Collections.Generic;

namespace AliShop.Domain.Catalog
{
    [Auditable]
    public class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int? ParentCatalogTypeId { get; set; }
        public CatalogType ParentCatalogName { get; set; }
        public ICollection<CatalogType> SubType { get; set; }
    }
}
