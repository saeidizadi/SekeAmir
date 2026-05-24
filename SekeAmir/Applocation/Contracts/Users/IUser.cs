
using Application.Contracts.Repository;
using Domain.Account;
using Domain.Dto.ViewModel.User;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.Contracts.Users
{
    public interface IUser
    {
        Task<bool> ISExistUserNameAsync(string userName);
        Task<bool> ISExistEmailAsync(string Email);

        Task<User> LoginCheckAsync(LoginViewModel model);
        Task<InformationUserViewModel> GetUserInformationAsync(string userName);

        Task<User> GetUserByUserNameAsync(string userName);

        int BalanceUserWallet(string userName);
        Task<UserPanelViewModel> GetUserPanelAsync(string userName);
        Task<bool> IsActiveCodeAsync(string code);
        Task<User> GetUserByActiveCodeAsync(string code);
        Task<User> UpdateAsync(User user);
        Task<User> AddUserAsync(User user);
        Task<IEnumerable<ShowUserBrifViewModel>> GetPaggingUserAsync(int Page, int pagesize);
        Task<IEnumerable<ShowUserBrifViewModel>> GetAllAdminAsync();
        Task<User> GetUserByUserId(int userId);
        Task<User> GetOrCreateUser(string phoneNumber);
        Task<IEnumerable<ShowUserBrifViewModel>> GetAllUser();

    }
}