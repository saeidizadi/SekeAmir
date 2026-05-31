using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Application.DTOs.Shop;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistence.Repository.Shop
{
    public class CategoryServices(IMaster<Category> master) : ICategory
    {
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await master.GetAllEfAsync();
        }

        public async Task<Category?> GetByApiId(int apiId)
        {
            var obj= await master.GetAllEfAsync(a => a.ApiId == apiId);
            return obj.FirstOrDefault();
        }

        public async Task<Category?> GetById(int id)
        {
            var obj = await master.GetAllEfAsync(a => a.id == id);
            return obj.FirstOrDefault() ;
        }

        public async Task<Category> InsertCategory(Category category)
        {
            var obj = await master.InsertAsync(category);
            return obj ;
        }

        public async Task<bool> IsExist(string title)
        {
            return await master.GetAllAsQueryable(a => a.title == title).AnyAsync();
        }

        public async Task<bool> UpdateCastegory(Category category)
        {
        var obj= await master.UpdateAsync(category);
            return obj!=null;
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
                    var category = new Category
                    {
                        ApiId = apiCategory.id,
                        iconImage = apiCategory.iconImage,
                        title = apiCategory.title,
                        description = apiCategory.description?.ToString(),
                        modifiedOn = apiCategory.modifiedOn
                    };
                    var result = await InsertCategory(category);
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
