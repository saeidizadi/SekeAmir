using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Shop
{
    public class EditProductVm
    {
        public int Id{ get; set; }
        [DisplayName("تصویر")]
        public string?  ImageAddress{ get; set; }
        public IFormFile? Image{ get; set; }
    }
}
