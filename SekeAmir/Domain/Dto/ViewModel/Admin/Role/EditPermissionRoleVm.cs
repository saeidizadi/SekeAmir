using Domain.Account.Permission;

namespace Domain.Dto.ViewModel.Admin.Role
{
    public class EditPermissionRoleVm
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<RolePermission> MenuVm { get; set; }
    }
}
