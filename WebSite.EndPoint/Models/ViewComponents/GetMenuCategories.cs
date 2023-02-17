using AliShop.Application.Catalogs.CatalogTypes.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Models.ViewComponents
{
  
    public class GetMenuCategories:ViewComponent
    {
        private readonly IGetMenuItemService getMenuItemService;

        public GetMenuCategories(IGetMenuItemService getMenuItemService)
        {
            this.getMenuItemService = getMenuItemService;
        }
       
    }
}
