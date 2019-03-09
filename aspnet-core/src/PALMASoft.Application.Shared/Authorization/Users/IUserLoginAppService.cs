using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization.Users.Dto;

namespace PALMASoft.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
