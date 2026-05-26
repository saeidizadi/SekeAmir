using Application.DTOs.Shop;
using Domain;
using Domain.Common;
using Domain.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Shop
{
    public interface IProductPrice
    {
        Task<bool> BulkInsertPrice(List<ProductPrice> productPrices);
        Task<bool> GetData();
        Task<IEnumerable<ProductPrice>> GetAllPrice();
        Task<IEnumerable<ProductPrice>> GetPriceByProdictId(int ProductId);

        Task<bool> InsertPrice(ProductPrice price);
        Task<IEnumerable<ShowAllPricesVM>> showAllPrices(InputType inputType);
        Task<List<ShowAllPricesVM>> ShowAllPrices();
        Task<PaginResult<ProductPrice>> GetProductPricesPagingAsync(int pageId, int take, int? productId = null);

    }
}
