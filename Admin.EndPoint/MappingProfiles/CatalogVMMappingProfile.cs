using Admin.EndPoint.ViewModels.Catalogs;
using AliShop.Application.Catalogs.CatalogTypes.CrudService;
using AutoMapper;

namespace Admin.EndPoint.MappingProfiles
{
    public class CatalogVMMappingProfile:Profile
    {
        public CatalogVMMappingProfile()
        {
            CreateMap<CatalogTypeDto, CatalogTypeViewModel>().ReverseMap();
        }
    }
}
