using System.ComponentModel.DataAnnotations;

namespace Domain.Account
{
  public  class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }
        public string GroupName { get; set; }
       
        public List<User> Users { get; set; }
    }
}
