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
        Task<Product> IsExist(int ItemId);
        Task<Product> InsertProduct(Product product);
        Task<IEnumerable<Product>> GetAll();
    }
}
