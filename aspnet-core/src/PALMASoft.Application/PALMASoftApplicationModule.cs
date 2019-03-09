using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PALMASoft.Authorization;

namespace PALMASoft
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(PALMASoftCoreModule)
        )]
    public class PALMASoftApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PALMASoftApplicationModule).GetAssembly());
        }
    }
}