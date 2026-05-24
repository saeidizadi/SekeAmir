using Microsoft.EntityFrameworkCore;
using Application.Contracts.Users;
using Domain.Account.Permission;
using Application.Contracts.Repository;
using Domain;

namespace Persistence.Repository.Users
{
    public class RolePermissionServices : IRolePermission
    {
        private IMaster<RolePermission> _master;


        public RolePermissionServices(IMaster<RolePermission> master)
        {
            _master = master;
        }
        public async Task<IEnumerable<RolePermission>> GetMenuOfRoleAsync(int RoleId)
        {
            return _master.GetAllAsQueryable(a => a.RoleId == RoleId && a.PermissionList.Status == (int)MenuStatus.menu)
                .Include(a => a.Role)
                .Include(a => a.PermissionList).ToList();

        }

        public async Task<IEnumerable<RolePermission>> GetPermissionOfRoleAsync(int RoleId)
        {

            return await _master.GetAllAsQueryable().Include(a => a.PermissionList).Where(a => a.RoleId == RoleId&&a.PermissionList.Status==(int)MenuStatus.permission).ToListAsync();
        }

        public async Task<bool> BulkInsertAsync(List<RolePermission> list)
        {
            return await _master.BulkeInsertAsync(list);
        }

        public async Task<bool> BulkDeleteAsync(List<RolePermission> list)
        {
            return await _master.BulkeDeleteAsync(list);
        }

        public async Task<IEnumerable<RolePermission>> getallAsync()
        {
            return await _master.GetAllEfAsync();
        }
    }
}

