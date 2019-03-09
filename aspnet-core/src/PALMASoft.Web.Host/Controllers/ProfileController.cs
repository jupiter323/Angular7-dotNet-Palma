using Abp.AspNetCore.Mvc.Authorization;
using PALMASoft.Storage;

namespace PALMASoft.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(ITempFileCacheManager tempFileCacheManager) :
            base(tempFileCacheManager)
        {
        }
    }
}