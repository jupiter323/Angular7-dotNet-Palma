using Abp.Authorization;
using PALMASoft.Authorization.Roles;
using PALMASoft.Authorization.Users;

namespace PALMASoft.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
