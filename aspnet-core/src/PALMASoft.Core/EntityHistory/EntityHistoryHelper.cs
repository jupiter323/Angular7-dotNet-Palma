using PALMASoft.Fincas;
using PALMASoft.Clientes;
using PALMASoft.Municipios;
using PALMASoft.Departamentos;
using PALMASoft.Paises;
using System;
using System.Linq;
using Abp.Organizations;
using PALMASoft.Authorization.Roles;
using PALMASoft.MultiTenancy;

namespace PALMASoft.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}
