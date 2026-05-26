using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Dapper;
using Domain;
using Domain.Common;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repository.Shop
{
    public class ProductPriceServices : IProductPrice
    {
        private readonly IMaster<ProductPrice> _master;
        private readonly ICategory _Category;
        private readonly IProduct _product;
        private readonly IMaster<ShowAllPricesVM> _Vm;

        public ProductPriceServices(IMaster<ProductPrice> master, ICategory category, IProduct product, IMaster<ShowAllPricesVM> vm)
        {
            _master = master;
            _Category = category;
            _product = product;
            _Vm = vm;
        }


        public async Task<bool> BulkInsertPrice(List<ProductPrice> productPrices)
        {
            return await _master.BulkeInsertAsync(productPrices);
        }

        public async Task<IEnumerable<ProductPrice>> GetAllPrice()
        {
            return await _master.GetAllAsQueryable().Include(a => a.Product).ThenInclude(a => a.Category).OrderByDescending(a => a.CreateAt).ToListAsync();
        }

        public async Task<bool> GetData()
        {

            using HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.sarafiyaran.com/api/itemtag/starred/1");
            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<ReciveDataApi>(json, options);
            if (data == null || data.result == null)
                return false;
            foreach (var category in data.result)
            {
                var DbCategory = await _Category.GetByApiId(category.ApiId);
                if (DbCategory == null)
                {
                    DbCategory = await _Category.InsertCategory(new Category()
                    {

                        ApiId = category.id,
                        iconImage = category.iconImage,
                        title = category.title,
                        description = category.description?.ToString(),
                        modifiedOn = category.modifiedOn
                    });
                }
                List<ProductPrice> List = new List<ProductPrice>();

                foreach (var item in category.products)
                {
                    var obj = await _product.IsExist(item.itemId);
                    if (obj == null)
                    {
                        obj = await _product.InsertProduct(new Product
                        {
                            itemId = item.itemId,
                            title = item.title.Trim(),
                            unit = item.unit,
                            nickName = item.nickName,
                            iconImage = item.iconImage,
                            CategoryId = DbCategory.id
                        });
                    }
                    List.Add(new ProductPrice()
                    {
                        BuyPrice = item.price1 == null ? 0 : item.price1.Value,
                        SellPrice = item.price2 == null ? 0 : item.price2.Value,
                        Change = item.change == null ? 0 : item.change.Value,
                        CreateAt = DateTime.Now,
                        inputType = (int)Domain.InputType.api,
                        ProductId = obj.id

                    });

                }
                if (!await _master.BulkeInsertAsync(List))
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<ProductPrice>> GetPriceByProdictId(int ProductId)
        {
            return await _master.GetAllAsQueryable(a => a.ProductId == ProductId).Include(a => a.Product).ThenInclude(a=>a.Category).OrderByDescending(a=>a.CreateAt).ToListAsync();
        }

        public async Task<bool> InsertPrice(ProductPrice price)
        {
            var obj = await _master.InsertAsync(price);
            return obj != null;
        }

        public async Task<IEnumerable<ShowAllPricesVM>> showAllPrices(InputType inputType)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("inputType", inputType, System.Data.DbType.Int32);
            var obj = await _Vm.GetAllAsync("showAllPrices", dynamicParameters);
            return obj;
        }

        public async Task<List<ShowAllPricesVM>> ShowAllPrices()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("inputType", inputType, System.Data.DbType.Int32);
            var obj = await _Vm.GetAllAsync("showAllPrices", dynamicParameters);
            return obj.ToList();
        }
        public async Task<PaginResult<ProductPrice>> GetProductPricesPagingAsync(int pageId, int take, int? productId = null)
        {
            Expression<Func<ProductPrice, bool>> filter = x => !productId.HasValue || x.ProductId == productId;
            return await _master.GetPagedAsync(
          pageId,
          take,
          orderBy: p => p.CreateAt,
          filter: filter,
          ascending: false,
          includes:new Expression<Func<ProductPrice, object>>[]
          {
              p=>p.Product,
              p=>p.Product.Category
          }
      );
        }
    }
}
