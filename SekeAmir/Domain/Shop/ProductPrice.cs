using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
    public class ProductPrice
    {
        public long Id { get; set; }
        [DisplayName("محصولات")]
        public int ProductId { get; set; }
        [DisplayName("قیمت خرید")]
        public decimal BuyPrice { get; set; }
        [DisplayName("قیمت فروش")]
        public decimal SellPrice { get; set; }
        public decimal Change { get; set; }
        public InputType inputType { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
