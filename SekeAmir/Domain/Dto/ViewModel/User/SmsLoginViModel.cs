using Domain.Tools;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.ViewModel.User
{
    public class SmsLoginViModel
    {
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        [Display(Name = "کاربر سیستم")]
        [MaxLength(11, ErrorMessage = "بیشتر مقدار{0}می باشد")]
        [IranianPhoneNumber]
        public string PhoneNumber { get; set; }
    }
    public class AcceptCodeViewModel
    {
        [Required(ErrorMessage = "وارد کردن {0} اجباری می باشد")]
        [Display(Name = "کد تایید")]
        public string SendCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
