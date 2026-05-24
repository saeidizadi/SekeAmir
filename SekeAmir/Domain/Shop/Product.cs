using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;


namespace Domain.Shop
{
    public class Product 
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public string title { get; set; }
        public string unit { get; set; }
        public string nickName { get; set; }
        public string iconImage { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category{ get; set; }

    }
}
