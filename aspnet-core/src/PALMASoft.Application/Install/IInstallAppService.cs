using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.Install.Dto;

namespace PALMASoft.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}