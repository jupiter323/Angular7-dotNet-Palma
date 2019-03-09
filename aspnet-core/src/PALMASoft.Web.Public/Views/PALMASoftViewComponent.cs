using Abp.AspNetCore.Mvc.ViewComponents;

namespace PALMASoft.Web.Public.Views
{
    public abstract class PALMASoftViewComponent : AbpViewComponent
    {
        protected PALMASoftViewComponent()
        {
            LocalizationSourceName = PALMASoftConsts.LocalizationSourceName;
        }
    }
}