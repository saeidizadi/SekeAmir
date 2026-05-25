using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
    public class ChangePrice
    {
        public int Id { get; set; }
        [DisplayName("محصولات")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product{ get; set; }
        [DisplayName("تغییرات خرید")]
        public int BuyChange{ get; set; }
        [DisplayName("تغییرات فروش")]
        public int SellChange{ get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
