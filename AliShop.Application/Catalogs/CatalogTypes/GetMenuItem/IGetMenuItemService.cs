using AliShop.Application.Contexts.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Catalogs.CatalogTypes.GetMenuItem
{
    public interface IGetMenuItemService
    {
        List<MenuItemDto> Execute();
    }
    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public GetMenuItemService(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public List<MenuItemDto> Execute()
        {
            var catalogType=context.CatalogType.Include(p=>p.ParentCatalogTypeId)
                .ToList(); 
            var data=mapper.Map<List<MenuItemDto>>(catalogType);
            return data;
        }
    }

    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<MenuItemDto> SubMenu { get; set; }
    }
}
