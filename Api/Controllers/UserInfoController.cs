using Api.Helpers;
using Api.Models;

namespace Api.Controllers
{
    public class UserInfoController : ControllerBase
    {
        public UserInfoModel Get()
        {
            var vkLogin = VkAuthHelper.GetCurrentUser(Request.Headers).VKLogin;
            return VkApiHelper.GetUserInfo(vkLogin);
        }
    }
}