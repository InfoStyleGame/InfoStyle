using System.Web.Http;
using Api.Helpers;
using Api.Managers;
using Api.Models;

namespace Api.Controllers
{
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public UserInfoViewModel Get()
        {
            var user = VkAuthHelper.GetCurrentUser(Request.Headers);
            var login = user.VKLogin;
            var info = VkApiHelper.GetUserInfo(login);
            return new UserInfoViewModel
            {
                Name = info.Name,
                PictureSrc = info.PictureSrc,
                Score = UserManager.GetUserScore(user.Id)
            };
        }

        [HttpGet]
        public bool IsLoggedIn()
        {
            return VkAuthHelper.GetCurrentUser(Request.Headers) != null;
        }
    }
}