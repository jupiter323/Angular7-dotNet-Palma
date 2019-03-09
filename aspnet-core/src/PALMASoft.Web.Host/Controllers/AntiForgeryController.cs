using Microsoft.AspNetCore.Antiforgery;

namespace PALMASoft.Web.Controllers
{
    public class AntiForgeryController : PALMASoftControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
