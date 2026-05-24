using Application.Contracts.Users;
using Domain;
using Domain.Dto.ViewModel.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Attribute;
using System.Security.Claims;

namespace SekeAmir.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private IUser _user;

        public AccountController(ILogger<AccountController> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }

        [Route("Logout")]
        public IActionResult LogOut()
        {
            _logger.LogInformation(EventIdList.Login, "User logged out. Session cleared");
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        [Route("Login")]
        [RediretAuthenticate("/Admin")]
        public IActionResult Login()
        {
            _logger.LogInformation(EventIdList.Login, "Admin login page requested"); 
            return View();

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(EventIdList.Login, "Invalid model state in admin login");
                return View(loginViewModel);
            }

            var user = await _user.LoginCheckAsync(loginViewModel);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور اشتباه می باشد");
                return View(loginViewModel);
            }
            else
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var Properties = new AuthenticationProperties()
                {
                    IsPersistent = loginViewModel.IsRemember
                };
                await HttpContext.SignInAsync(principal, Properties);
                _logger.LogInformation(EventIdList.Login, "Admin user {UserId} logged in successfully", user.UserId);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

        }

    }
}
