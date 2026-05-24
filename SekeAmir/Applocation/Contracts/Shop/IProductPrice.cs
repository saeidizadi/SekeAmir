using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shop;

namespace Application.Contracts.Shop
{
    public interface IProductPrice
    {
        Task<bool> BulkInsertPrice(List<ProductPrice> productPrices);
        Task<bool> GetData();
        Task<IEnumerable<ProductPrice>> GetAllPrice();
        Task<bool> InsertPrice(ProductPrice price);
    }
}
