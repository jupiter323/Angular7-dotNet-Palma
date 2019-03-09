using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PALMASoft
{
    public class PALMASoftClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PALMASoftClientModule).GetAssembly());
        }
    }
}
