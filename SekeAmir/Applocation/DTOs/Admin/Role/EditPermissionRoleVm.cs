using Domain.Account.Permission;

namespace Application.DTOs.Admin.Role
{
    public class EditPermissionRoleVm
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<RolePermission> MenuVm { get; set; }
    }
}
