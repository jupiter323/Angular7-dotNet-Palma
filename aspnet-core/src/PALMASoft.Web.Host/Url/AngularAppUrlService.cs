using Abp.MultiTenancy;
using PALMASoft.Url;

namespace PALMASoft.Web.Url
{
    public class AngularAppUrlService : AppUrlServiceBase
    {
        private const string V = "account/reset-password";

        public override string EmailActivationRoute => "account/confirm-email";

        public override string PasswordResetRoute => V;

        public AngularAppUrlService(
                IWebUrlService webUrlService,
                ITenantCache tenantCache
            ) : base(
                webUrlService,
                tenantCache
            )
        {

        }
    }
}