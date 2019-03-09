using System.Threading.Tasks;
using PALMASoft.Security.Recaptcha;

namespace PALMASoft.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
