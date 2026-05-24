using Domain.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Shop
{
    public interface ICategory
    {
        Task<bool> UpgradeCategory();
        Task<bool> IsExist(string Title);
        Task<Category> InsertCategory(Category category);
        Task<Category> GetByApiId(int  apiId);
        Task<IEnumerable<Category>> GetAll();
    }
}
