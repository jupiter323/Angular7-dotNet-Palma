using System.Collections.Generic;
using PALMASoft.Authorization.Permissions.Dto;

namespace PALMASoft.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}