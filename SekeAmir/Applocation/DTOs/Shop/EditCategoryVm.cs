using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Shop
{
    public class EditCategoryVm
    {
     
        public int id { get; set; }

        [DisplayName("توضیحات")]
        public string? description { get; set; }
        [DisplayName("تصویر")]
        public string? iconImage { get; set; }
        
        public IFormFile? Image{ get; set; }
    }
}
