using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
    public class Category
    {

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string iconImage { get; set; }
        public DateTime modifiedOn { get; set; }
        public List<Product> products{ get; set; }
    }
}
