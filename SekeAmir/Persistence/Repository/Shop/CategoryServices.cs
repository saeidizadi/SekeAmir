using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repository.Shop
{
    public class CategoryServices : ICategory
    {
        private readonly IMaster<Category> _master;

        public CategoryServices(IMaster<Category> master)
        {
            _master = master;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _master.GetAllEfAsync();
        }

        public async Task<Category> GetByApiId(int apiId)
        {
            var obj= await _master.GetAllEfAsync(a => a.ApiId == apiId);
            return obj?.FirstOrDefault();
        }

        public async Task<Category> InsertCategory(Category category)
        {
            var obj = await _master.InsertAsync(category);
            return obj ;
        }

        public async Task<bool> IsExist(string Title)
        {
            return await _master.GetAllAsQueryable(a => a.title == Title).AnyAsync();
        }

        public async Task<bool> UpgradeCategory()
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
            foreach (var apiCategory in data.result)
            {
                // چک میکنه قبلا وجود داشته یا نه
                var dbCategory = await IsExist(apiCategory.title);

                // اگر نبود اضافه کن
                if (!dbCategory)
                {
                    var newcategory = new Category
                    {
                        ApiId = apiCategory.id,
                        iconImage = apiCategory.iconImage,
                        title = apiCategory.title,
                        description = apiCategory.description?.ToString(),
                        modifiedOn = apiCategory.modifiedOn
                    };
                    var result = await InsertCategory(newcategory);
                    if (result == null)
                    {
                        return false;
                    }

                }



            }
            return true;

        }
    }
}
