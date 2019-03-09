using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PALMASoft
{
    [DependsOn(typeof(PALMASoftXamarinSharedModule))]
    public class PALMASoftXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PALMASoftXamarinAndroidModule).GetAssembly());
        }
    }
}