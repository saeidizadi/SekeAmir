using Domain.Account;
using Domain.Account.Permission;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class SekeAminContext(DbContextOptions<SekeAminContext> options):DbContext(options)
    {
        public virtual DbSet<User> MyUser { get; set; }
        public virtual DbSet<PermissionList> PermissionList { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }




        #region Shop
        public virtual DbSet<Category> Categories{ get; set; }
        #endregion
    }
}
