using Domain.Account;

namespace Application.DTOs.Admin.Role
{
    public class EditUserRoleVm
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
