using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shop
{
    public class ShowAllPricesVM
    {
        public int ProductId { get; set; }
        [DisplayName("محصول")]
        public string ProductTitle { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("دسته بندی")]

        public string CategoryTitle { get; set; }
        public string description { get; set; }
        public string iconimage { get; set; }
        [DisplayName("قیمت دریافتی فروش")]
        public decimal BaseSellPrice { get; set; }
        [DisplayName("تغییرات قیمت فروش")]
        public decimal SellChange { get; set; }
        [DisplayName("تغییرات قیمت فروش")]
        public decimal FinalSellPrice { get; set; }
        [DisplayName("قیمت دریافتی خرید")]
        public decimal BaseBuyPrice { get; set; }
        [DisplayName("تغییرات قیمت خرید")]
        public decimal buyChange { get; set; }
        [DisplayName("قیمت نهایی خرید")]
        public decimal FinalBuyPrice { get; set; }
    }
}
