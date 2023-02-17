using AliShop.Application.Catalogs.CatalogTypes.CrudService;
using AliShop.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class IndexModel : PageModel
    {

        private readonly ICatalogTypeService catalogTypeService;

        public IndexModel(ICatalogTypeService catalogTypeService)
        {
            this.catalogTypeService = catalogTypeService;
        }

        public PaginatedItemDto<CatalogTypeListDto> CataolType { get; set; }
        public void OnGet(int? parentId, int page = 1, int pageSize = 100)
        {
            CataolType = catalogTypeService.GetList(parentId, page, pageSize);
        }
    }
}

