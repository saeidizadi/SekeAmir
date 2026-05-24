using Domain.Account;
using Domain.Account.Permission;

namespace Application.Contracts.Users
{
    public interface IRole
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role> GetRoleAsync(int RoleId);
        Task<Role> insertAsync(Role role);
        Task<Role> updateAsync(Role role);
        Task<bool> deleteAsync(Role role);

        Task<IEnumerable<UserRole>> GetAllUSerOfRoleAsync(int RoleId);
        Task<bool> BulkDeleteAsync(IEnumerable<UserRole> List);
        Task<bool> BulkInsertAsync(IEnumerable<UserRole> List);
        Task<IEnumerable<UserRole>> GetAllUserRoleAsync();
       Task<bool> UserRoleInsertAsync(UserRole userRole);
    }
}