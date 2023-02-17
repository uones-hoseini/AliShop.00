using AliShop.Application.Catalogs.CatalogTypes.CrudService;
using AliShop.Domain.Catalog;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Infrustructure.IdentityConfigs.MappingProfile
{
    public class CatalogMappingProfile:Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

            CreateMap<CatalogType, CatalogTypeListDto>()
              .ForMember(dest => dest.SubTypeCount, option =>
               option.MapFrom(src => src.SubType.Count));
        }
    }
}
