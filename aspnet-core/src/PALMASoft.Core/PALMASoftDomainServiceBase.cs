using Abp.Domain.Services;

namespace PALMASoft
{
    public abstract class PALMASoftDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PALMASoftDomainServiceBase()
        {
            LocalizationSourceName = PALMASoftConsts.LocalizationSourceName;
        }
    }
}
