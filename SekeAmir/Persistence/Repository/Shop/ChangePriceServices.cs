using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.Shop
{
    public class ChangePriceServices : IChangePrice
    {
        private readonly IMaster<ChangePrice> _Master;

        public ChangePriceServices(IMaster<ChangePrice> master)
        {
            _Master = master;
        }

        public async Task<IEnumerable<ChangePrice>> GetAllChangePrice()
        {
           return await _Master.GetAllAsQueryable().Include(a=>a.Product).ToListAsync();
        }

        public async Task<bool> InsertChange(ChangePrice changePrice)
        {
           var obj=await _Master.InsertAsync(changePrice);
            return obj != null;
        }
    }
}
