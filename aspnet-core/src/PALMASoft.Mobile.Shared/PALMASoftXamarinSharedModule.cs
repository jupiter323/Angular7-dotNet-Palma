using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PALMASoft
{
    [DependsOn(typeof(PALMASoftClientModule), typeof(AbpAutoMapperModule))]
    public class PALMASoftXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PALMASoftXamarinSharedModule).GetAssembly());
        }
    }
}