using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.Sessions.Dto;

namespace PALMASoft.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
