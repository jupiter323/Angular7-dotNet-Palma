using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace PALMASoft.Web.Controllers
{
    public class HomeController : PALMASoftControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
