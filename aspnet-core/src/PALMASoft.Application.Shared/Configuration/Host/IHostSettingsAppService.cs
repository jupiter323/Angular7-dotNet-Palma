using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.Configuration.Host.Dto;

namespace PALMASoft.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
