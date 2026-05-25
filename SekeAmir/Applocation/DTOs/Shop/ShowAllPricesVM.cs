using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shop
{
    public class ShowAllPricesVM
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string description { get; set; }
        public string iconimage { get; set; }
        public decimal BaseSellPrice { get; set; }
        public decimal SellChange { get; set; }
        public decimal FinalSellPrice { get; set; }
        public decimal BaseBuyPrice { get; set; }
        public decimal buyChange { get; set; }
        public decimal FinalBuyPrice { get; set; }
    }
}
