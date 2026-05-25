using Domain.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Shop
{
    public interface IChangePrice
    {
        Task<bool> InsertChange(ChangePrice changePrice);
        Task<IEnumerable<ChangePrice>> GetAllChangePrice();
    }
}
