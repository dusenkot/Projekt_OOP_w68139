using System;
namespace Projekt
{
    public class UserWithRolesAndPrivileges
    {
        public User User { get; set; }
        public List<UserRole> Roles { get; set; }
        public List<UserPrivilege> Privileges { get; set; }
    }
}

