using Microsoft.Extensions.Configuration;

namespace PALMASoft.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
