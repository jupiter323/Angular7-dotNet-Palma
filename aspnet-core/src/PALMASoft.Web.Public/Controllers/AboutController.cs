using Microsoft.AspNetCore.Mvc;
using PALMASoft.Web.Controllers;

namespace PALMASoft.Web.Public.Controllers
{
    public class AboutController : PALMASoftControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}