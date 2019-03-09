using System.Threading.Tasks;
using Abp.Application.Services;
using PALMASoft.MultiTenancy.Payments.Dto;
using PALMASoft.MultiTenancy.Payments.PayPal.Dto;

namespace PALMASoft.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalPaymentId, string paypalPayerId);

        PayPalConfigurationDto GetConfiguration();

        Task CancelPayment(CancelPaymentDto input);
    }
}
