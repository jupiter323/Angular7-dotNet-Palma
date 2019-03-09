using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PALMASoft
{
    [DependsOn(typeof(PALMASoftXamarinSharedModule))]
    public class PALMASoftXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PALMASoftXamarinIosModule).GetAssembly());
        }
    }
}