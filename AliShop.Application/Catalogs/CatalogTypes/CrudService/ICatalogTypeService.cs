using AliShop.Application.Contexts.Interfaces;
using AliShop.Application.Dtos;
using AliShop.Domain.Catalog;
using AutoMapper;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Catalogs.CatalogTypes.CrudService
{
    public interface ICatalogTypeService
    {
        BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType);
        BaseDto Remove(int Id);
        BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType);
        BaseDto<CatalogTypeDto> FindById(int Id);
        PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize);
    }
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;

        public CatalogTypeService(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType)
        {
            var model = mapper.Map<CatalogType>(catalogType);
            context.CatalogType.Add(model);
            context.SaveChanges();
            return new BaseDto<CatalogTypeDto>(
                true,
                new List<string> { $"تایپ {model.Type}  با موفقیت در سیستم ثبت شد" },
                mapper.Map<CatalogTypeDto>(model)
                );
        }

        public BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType)
        {
            var model = context.CatalogType.SingleOrDefault(p => p.Id == catalogType.Id);
            mapper.Map(catalogType, model);
            context.SaveChanges();
            return new BaseDto<CatalogTypeDto>
              (
               true,
                new List<string> { $"تایپ {model.Type} با موفقیت ویرایش شد" },

                mapper.Map<CatalogTypeDto>(model)
              );
        }

        public BaseDto<CatalogTypeDto> FindById(int Id)
        {
            var data = context.CatalogType.Find(Id);
            var result = mapper.Map<CatalogTypeDto>(data);
            return new BaseDto<CatalogTypeDto>
            (
                true,
                null,
                result
            );
        }

        public PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {
            int totalCount = 0;
            var model = context.CatalogType
                .Where(p => p.ParentCatalogTypeId == parentId)
                .PagedResult(page, pageSize, out totalCount);
            var result = mapper.ProjectTo<CatalogTypeListDto>(model).ToList();
            return new PaginatedItemDto<CatalogTypeListDto>(page, pageSize, totalCount, result);
        }

        public BaseDto Remove(int Id)
        {
            var catalogType = context.CatalogType.Find(Id);
            context.CatalogType.Remove(catalogType);
            context.SaveChanges();
            return new BaseDto
            (
             true,
             new List<string> { $"ایتم با موفقیت حذف شد" }
             );
        }
    }
    public class CatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }
    }


    public class CatalogTypeListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int SubTypeCount { get; set; }
    }
}
