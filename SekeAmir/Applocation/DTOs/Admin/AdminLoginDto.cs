using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Admin;

public class AdminLoginDto
{
    [Required(ErrorMessage = "نام کاربری الزامی است")]
    [Display(Name = "نام کاربری")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین 3 تا 50 کاراکتر باشد")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "رمز عبور الزامی است")]
    [Display(Name = "رمز عبور")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "کد امنیتی الزامی است")]
    [Display(Name = "کد امنیتی")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "کد امنیتی باید 5 کاراکتر باشد")]
    public string CaptchaInput { get; set; } = string.Empty;

    [Display(Name = "مرا به خاطر بسپار")]
    public bool RememberMe { get; set; }
}

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "ایمیل یا شماره موبایل الزامی است")]
    [Display(Name = "ایمیل یا شماره موبایل")]
    public string EmailOrPhone { get; set; } = string.Empty;
}

//public class ResetPasswordViewModel
//{
//    [Required]
//    public string Token { get; set; } = string.Empty;

//    [Required(ErrorMessage = "رمز عبور جدید الزامی است")]
//    [DataType(DataType.Password)]
//    [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
//    [Display(Name = "رمز عبور جدید")]
//    public string NewPassword { get; set; } = string.Empty;

//    [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
//    [DataType(DataType.Password)]
//    [Compare("NewPassword", ErrorMessage = "رمز عبور و تکرار آن مطابقت ندارند")]
//    [Display(Name = "تکرار رمز عبور")]
//    public string ConfirmPassword { get; set; } = string.Empty;
//}

//public class User
//{
//    public int Id { get; set; }
//    public string Username { get; set; } = string.Empty;
//    public string PasswordHash { get; set; } = string.Empty;
//    public string Email { get; set; } = string.Empty;
//    public string PhoneNumber { get; set; } = string.Empty;
//    public string FullName { get; set; } = string.Empty;
//    public string Role { get; set; } = "User"; // Admin, Manager, User
//    public bool IsActive { get; set; } = true;
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//    public DateTime? LastLoginAt { get; set; }
//}