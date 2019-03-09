using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.Editions.Dto;
using PALMASoft.MultiTenancy.Dto;

namespace PALMASoft.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}