using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using PALMASoft.Authorization.Users;
using PALMASoft.MultiTenancy;

namespace PALMASoft.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}