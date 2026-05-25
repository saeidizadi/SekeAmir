using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class ShowUserBrifViewModel
    {
        public int UserId { get; set; }
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [DisplayName("ایمیل")]
        public string Email { get; set; }
        [DisplayName("نام کاربر")]
        public string FullName { get; set; }

        public string DisplayName => $"{UserName} - {FullName}";
    }
}
