using Abp.Application.Services;
using PALMASoft.Dto;
using PALMASoft.Logging.Dto;

namespace PALMASoft.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
