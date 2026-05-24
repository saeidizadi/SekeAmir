

using Domain.Account.Permission;

namespace Application.Contracts.Users
{
    public interface IRolePermission
    {
        Task<IEnumerable<RolePermission>> GetMenuOfRoleAsync(int RoleId);
        Task<IEnumerable<RolePermission>> GetPermissionOfRoleAsync(int RoleId);
        Task<IEnumerable<RolePermission>> getallAsync();
        Task<bool> BulkInsertAsync(List<RolePermission> list);

        Task<bool> BulkDeleteAsync(List<RolePermission> list);
    }
}