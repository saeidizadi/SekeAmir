using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
    public class ChangePrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product{ get; set; }
        public int BuyChange{ get; set; }
        public int SellChange{ get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
