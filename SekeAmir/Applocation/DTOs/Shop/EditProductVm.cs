using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shop
{
    public class EditProductVm
    {
        public int Id{ get; set; }
        [DisplayName("تصویر")]
        public string?  ImageAddress{ get; set; }
        public IFormFile? Image{ get; set; }
        [DisplayName("وضعیت معامله")]
        public bool IsExchange { get; set; }
        [DisplayName("منبع نمایش")]
        public InputType InputType { get; set; }
    }
}
