using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shop;

namespace Application.Contracts.Shop
{
    public interface IProduct
    {
        Task<bool> UpgradeProduct();
        Task<bool> IsExist(int ItemId);
        Task<bool> InsertProduct(Product product);
        Task<IEnumerable<Product>> GetAll();
    }
}
