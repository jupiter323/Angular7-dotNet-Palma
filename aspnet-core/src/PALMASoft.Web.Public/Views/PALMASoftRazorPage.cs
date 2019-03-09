using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace PALMASoft.Web.Public.Views
{
    public abstract class PALMASoftRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected PALMASoftRazorPage()
        {
            LocalizationSourceName = PALMASoftConsts.LocalizationSourceName;
        }
    }
}
