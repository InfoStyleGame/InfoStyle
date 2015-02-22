using System.Web.Http;
using Api.Helpers;
using Api.Models;

namespace Api.Controllers
{
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public UserInfoModel Get()
        {
            var vkLogin = VkAuthHelper.GetCurrentUser(Request.Headers).VKLogin;
            return VkApiHelper.GetUserInfo(vkLogin);
        }

        [HttpGet]
        public bool IsLoggedIn()
        {
            return VkAuthHelper.GetCurrentUserReally(Request.Headers) != null;
        }
    }
}