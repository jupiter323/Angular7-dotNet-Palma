using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.Configuration.Tenants.Dto;

namespace PALMASoft.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
