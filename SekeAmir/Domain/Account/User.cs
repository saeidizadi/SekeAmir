
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Account
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        [Display(Name = "کاربر سیستم")]
        [MaxLength(50, ErrorMessage = "بیشتر مقدار{0}می باشد")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "فرمت وراد صحیح نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "رمزعبور")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        public string PassWord { get; set; }
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }
        public string UserAvatar { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "کاربر")]
        public string FullName { get; set; }
 
        public virtual List<UserRole> UserRoles { get; set; }


    }



}

