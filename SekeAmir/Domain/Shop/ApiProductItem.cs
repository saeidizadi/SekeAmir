using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
    public class ApiProductItem
    {
        public int itemId { get; set; }
        public string title { get; set; }
        public string unit { get; set; }
        public string nickName { get; set; }
        public string iconImage { get; set; }

        // قیمت‌ها (بر اساس چیزی که API برمی‌گرداند)
        public decimal? price1 { get; set; }       // مثلا Buy
        public decimal? price2 { get; set; }       // مثلا Sell
        public decimal? priceLast1 { get; set; }
        public decimal? priceLast2 { get; set; }
        public decimal? change { get; set; }
    }
}
