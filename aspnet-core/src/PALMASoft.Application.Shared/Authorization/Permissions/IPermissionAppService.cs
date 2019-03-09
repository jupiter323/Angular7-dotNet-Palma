using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization.Permissions.Dto;

namespace PALMASoft.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
