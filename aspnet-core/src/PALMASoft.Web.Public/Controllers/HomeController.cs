using Microsoft.AspNetCore.Mvc;
using PALMASoft.Web.Controllers;

namespace PALMASoft.Web.Public.Controllers
{
    public class HomeController : PALMASoftControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}