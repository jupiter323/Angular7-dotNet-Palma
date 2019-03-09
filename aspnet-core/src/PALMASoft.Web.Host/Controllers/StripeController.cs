using PALMASoft.MultiTenancy.Payments.Stripe;

namespace PALMASoft.Web.Controllers
{
    public class StripeController : StripeControllerBase
    {
        public StripeController(
            StripeGatewayManager stripeGatewayManager,
            StripePaymentGatewayConfiguration stripeConfiguration)
            : base(stripeGatewayManager, stripeConfiguration)
        {
        }
    }
}
