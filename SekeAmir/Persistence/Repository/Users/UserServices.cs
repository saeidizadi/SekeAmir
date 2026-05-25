using Application.Contracts.Repository;
using Application.Contracts.Users;
using Application.DTOs.User;
using Domain.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.Users
{
    public class UserServices : IUser
    {
        private readonly IMaster<User> _User;
        private readonly IRole _Role;

        public UserServices(IMaster<User> user, IRole role)
        {
            _User = user;
            _Role = role;
        }



        public async Task<bool> ISExistUserNameAsync(string userName)
        {
            var obj = await _User.GetAllEfAsync(a => a.UserName == userName);
            return obj.Any();
        }

        public async Task<bool> ISExistEmailAsync(string Email)
        {
            var obj = await _User.GetAllEfAsync(a => a.Email == StringTools.FixEmail(Email));
            return obj.Any();
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _User.InsertAsync(user);
        }

        public async Task<User> LoginCheckAsync(LoginViewModel model)
        {
            var a = PasswordHelper.EncodePasswordMD5(model.PassWord);
            var obj = await _User.GetAllEfAsync(a =>
                a.UserName == StringTools.FixEmail(model.UserName) &&
                a.PassWord == PasswordHelper.EncodePasswordMD5(model.PassWord) & a.IsActive == true);
            return obj.FirstOrDefault();
        }

        public async Task<InformationUserViewModel> GetUserInformationAsync(string userName)
        {
            var user = await GetUserByUserNameAsync(userName);
            var infomation = new InformationUserViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                RegisterDate = user.RegisterDate,
                Wallet = 0
            };
            return infomation;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var obj = await _User.GetAllEfAsync(a => a.UserName == StringTools.FixEmail(userName));
            return obj.SingleOrDefault();
        }

        public int BalanceUserWallet(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPanelViewModel> GetUserPanelAsync(string userName)
        {
            var obj = await _User.GetAllEfAsync(a => a.UserName == StringTools.FixEmail(userName));
            return obj.Select(u => new UserPanelViewModel()
            {
                Name = u.Email,
                Image = u.UserAvatar
            }).Single();
        }

        public async Task<bool> IsActiveCodeAsync(string code)
        {
            var obj = await _User.GetAllEfAsync(a => a.ActiveCode == code);
            return obj.Any();
        }

        public async Task<User> GetUserByActiveCodeAsync(string code)
        {
            var obj = await _User.GetAllEfAsync(a => a.ActiveCode == code);
            return obj.FirstOrDefault();
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _User.UpdateAsync(user);
        }

        public async Task<IEnumerable<ShowUserBrifViewModel>> GetPaggingUserAsync(int Page, int pagesize)
        {
            var obj = await _User.GetPagingAsync(Page, pagesize);
            return obj.Select(a => new ShowUserBrifViewModel() { Email = a.Email, UserName = a.UserName, UserId = a.UserId, FullName = a.FullName });
        }

        public async Task<IEnumerable<ShowUserBrifViewModel>> GetAllAdminAsync()
        {
            var obj = await _User.GetAllEfAsync(a => a.IsAdmin == true && a.Email != "ali.mottaghi1991@gmail.com");
            return obj.Select(a => new ShowUserBrifViewModel() { Email = a.Email, UserName = a.UserName, UserId = a.UserId, FullName = a.FullName });
        }
        public async Task<User> GetUserByUserId(int userId)
        {
            var obj = _User.GetAllAsQueryable(a => a.UserId == userId)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.PermissionList);
            return obj.FirstOrDefault();
        }

        public async Task<User> GetOrCreateUser(string phoneNumber)
        {
            var user = await GetUserByUserNameAsync(phoneNumber);
            if (user != null)
                return user;

            var newUser = new User
            {
                UserName = phoneNumber,
                Email = "",
                IsActive = true,
                PassWord = PasswordHelper.EncodePasswordMD5(CodeGenerator.Generate()),
                RegisterDate = DateTime.Now,
                UserAvatar = "default.jpg",
                IsAdmin = false,
                ActiveCode = StringTools.GenerateUniqeCode()
            };

            var result = await AddUserAsync(newUser);
            await _Role.UserRoleInsertAsync(new UserRole { RoleId = 4, UserId = result.UserId });

            return result;
        }

        public async Task SignIn(HttpContext context, User user)
        {
            if (user.FullName == null)
            {
                user.FullName = "";
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),

            new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role,"User")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties { IsPersistent = true };
            await context.SignInAsync(principal, properties);
        }

        public async Task<IEnumerable<ShowUserBrifViewModel>> GetAllUser()
        {
            var obj = await _User.GetAllEfAsync(a => a.IsAdmin == false);
            return obj.Select(x => new ShowUserBrifViewModel()
            {
                UserName = x.UserName,
                Email = x.Email,
                UserId = x.UserId,
                FullName = x.FullName
            });
        }
    }
}
