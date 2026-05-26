using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Domain.Common;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using Persistence.Migrations;

namespace Persistence.Repository.Shop
{
    public class ProductServices : IProduct
    {
        private readonly IMaster<Product> _master;
        private readonly ICategory _category;

        public ProductServices(IMaster<Product> master, ICategory category)
        {
            _master = master;
            _category = category;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _master.GetAllAsQueryable().Include(a => a.Category).ToListAsync();
        }

        public async Task<List<Product>> GetAllWithPrice(Expression<Func<Product, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int Id)
        {
            var obj = await _master.GetAllEfAsync(a => a.id == Id);
            return obj.FirstOrDefault();
        }

 

        public async Task<Product> InsertProduct(Product product)
        {
           var obj= await _master.InsertAsync(product);
            return obj;
        }

        public async Task<Product> IsExist(int ItemId)
        {
            var obj = await _master.GetAllEfAsync(a => a.itemId == ItemId);
            return obj.FirstOrDefault();
        }

        public async Task<bool> updateProduct(Product product)
        {
            var obj = await _master.UpdateAsync(product);
            return obj != null;
        }

        public async Task<bool> UpgradeProduct()
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
                var DbCategory = await _category.GetByApiId(category.ApiId);
                if (DbCategory == null)
                {
                    DbCategory = await _category.InsertCategory(new Category()
                    {

                        ApiId = category.id,
                        iconImage = category.iconImage,
                        title = category.title,
                        description = category.description?.ToString(),
                        modifiedOn = category.modifiedOn
                    });
                }
                foreach (var item in category.products)
                {
                    var Dbproduct = await IsExist(item.itemId);
                    if (Dbproduct == null)
                    {
                     await  InsertProduct(new Product
                        {
                            itemId = item.itemId,
                            title = item.title.Trim(),
                            unit = item.unit,
                            nickName = item.nickName,
                            iconImage = item.iconImage,
                            CategoryId = DbCategory.id
                        });
                    }
                }
            }
            return true;
        }
    }
}
